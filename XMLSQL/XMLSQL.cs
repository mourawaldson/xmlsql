using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Threading;
using System.IO;

namespace XMLSQL
{
    public partial class XMLSQL : Form
    {
        public XMLSQL()
        {
            InitializeComponent();
        }

        DB objCon = new DB();
        public string tipoBanco;
        public string banco;
        public bool dbcorrupted = false;
        Functions objFunc = new Functions();
        string fileNameXml = "config.xml";

        #region checkDBSQLServer
        private void checkDB()
        {
            string banco = this.banco;

            string SQL = "BEGIN TRY DECLARE @Return INT EXEC @Return = sp_executesql N'DBCC CHECKDB(''" + banco + "'') WITH NO_INFOMSGS' IF @Return <> 0 RAISERROR ('Database corrupt', 11, 1) END TRY BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_SEVERITY() AS ErrorSeverity, ERROR_STATE() AS ErrorState, ERROR_PROCEDURE() AS ErrorProcedure, ERROR_LINE() AS ErrorLine, ERROR_MESSAGE() AS ErrorMessage; END CATCH";

            ArrayList array = new ArrayList();

            array = this.objCon.getResult2(SQL);

            if (array.Count != 0)
            {
                MessageBox.Show("O banco em questão está corrompido!", "Acesso Negado!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.dbcorrupted = true;
                return;
            }

        }
        #endregion

        #region GerarXml SQL Server
        protected void gerarXML()
        {
            try
            {
                string nomePasta = DateTime.Today.ToString("dd-MM-yyyy");

                System.IO.Directory.CreateDirectory(nomePasta);

                DataTable tabelas = new DataTable();

                if (this.tipoBanco == "SQL Server")
                {
                    tabelas = this.objCon.getResult(this.objCon.SQLTablesSQLServer);
                }
                else if (this.tipoBanco == "Firebird")
                {
                    tabelas = this.objCon.getResult(this.objCon.SQLTablesFirebird);
                }

                XmlTextWriter xml;

                int qtdTabelas = tabelas.Rows.Count;

                for (int posicaoTabela = 0; posicaoTabela < qtdTabelas; posicaoTabela++)
                {
                    string nomeTabela = tabelas.Rows[posicaoTabela][0].ToString().Trim();

                    if (nomeTabela.IndexOf(" ") == -1)
                    {
                        string SQLRows = "SELECT * FROM " + nomeTabela;

                        DataTable tableQtdRegistros = new DataTable();
                        tableQtdRegistros = this.objCon.getResult(SQLRows);

                        int qtdRegistros = tableQtdRegistros.Rows.Count;

                        xml = new XmlTextWriter(nomePasta + "/" + nomeTabela + ".xml", Encoding.UTF8);
                        xml.WriteStartDocument();
                        xml.Formatting = Formatting.Indented;
                        xml.WriteStartElement("efiscRequest");
                        xml.WriteStartElement("importRequest");

                        for (int linha = 0; linha < qtdRegistros; linha++)
                        {
                            xml.WriteStartElement(nomeTabela.ToLower());

                            string SQLColumn = "";

                            if (this.tipoBanco == "SQL Server")
                            {
                                SQLColumn = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + nomeTabela + "'";
                            }
                            else if (this.tipoBanco == "Firebird")
                            {
                                SQLColumn = "SELECT DISTINCT TRIM(RDB$RELATION_FIELDS.RDB$FIELD_NAME) FROM RDB$RELATION_FIELDS, RDB$FIELDS WHERE ( RDB$RELATION_FIELDS.RDB$FIELD_SOURCE = RDB$FIELDS.RDB$FIELD_NAME ) AND RDB$RELATION_FIELDS.RDB$RELATION_NAME = '" + nomeTabela + "' AND ( RDB$FIELDS.RDB$SYSTEM_FLAG <> 1 )";
                            }

                            DataTable colunasNomes = new DataTable();
                            colunasNomes = this.objCon.getResult(SQLColumn);

                            int qtdColunas = colunasNomes.Rows.Count;

                            for (int coluna = 0; coluna < qtdColunas; coluna++)
                            {
                                string valor = tableQtdRegistros.Rows[linha][coluna].ToString();

                                string elemento = colunasNomes.Rows[coluna][0].ToString().ToLower();

                                if (valor != "" || valor == null)
                                {
                                    xml.WriteElementString(elemento, valor);
                                }
                            }

                            xml.WriteEndElement();
                            colunasNomes.Clear();
                        }
                        tableQtdRegistros.Clear();
                        tableQtdRegistros.Reset();
                        xml.WriteEndElement();
                        xml.WriteEndElement();
                        xml.Flush();
                        xml.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar xml!" + ex.ToString());
            }
            finally
            {
               this.objCon.close();
            }
        }
        #endregion

        #region checkTipoBanco
        protected void checkTipoBanco()
        {
            if (this.cbTipoBanco.SelectedIndex == 0)
            {
                this.btnIniciar.Visible = true;
                this.btnCancel.Visible = false;
                this.btnGerarXmlTxt.Visible = false;
            }
            else if (this.cbTipoBanco.SelectedIndex == 1)
            {
                this.btnIniciar.Visible = true;
                this.btnCancel.Visible = false;
                this.btnGerarXmlTxt.Visible = false;
            }
            else if (this.cbTipoBanco.SelectedIndex == 2)
            {
                this.btnIniciar.Visible = false;
                this.btnCancel.Visible = false;
                this.btnGerarXmlTxt.Visible = true;
            }
        }
        #endregion

        #region Load do Form
        private void XMLSQL_Load(object sender, EventArgs e)
        {
            this.cbTipoBanco.SelectedIndex = 0;
        }
        #endregion

        #region Botão Iniciar
        private void btnConnectSQLServer_Click(object sender, EventArgs e)
        {
            if (File.Exists("config.xml"))
            {
                this.tipoBanco = this.cbTipoBanco.SelectedItem.ToString();
                this.cbTipoBanco.Enabled = false;
                this.btnCancel.Visible = true;
                this.btnIniciar.Visible = false;

                timerXMLSQL.Enabled = true;
                Thread thread = new Thread(new ThreadStart(timerXMLSQL.Start));
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                MessageBox.Show("O arquivo de configuração não foi encontrado!");
            }
        }
        #endregion

        #region Botão Desconectar
        private void btnDisconnectSQLServer_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Caso exista uma operação em andamento, a mesma será finalizada e o programa encerrado! Deseja confirmar esta operação?","Confirmação",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        #endregion

        #region CheckTipoBanco

        private void cbTipoBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkTipoBanco();
        }
        #endregion

        private void timerXMLSQL_Tick(object sender, EventArgs e)
        {
            string horaAtual = DateTime.Now.ToLongTimeString().ToString();

            string horaXML = this.objFunc.LoadXmlConfig(fileNameXml, "hora").GetValue(0).ToString();

            if (horaXML == horaAtual)
            {

                if (this.tipoBanco == "SQL Server")
                {
                    String serverAddress, authentication, isAuthentication, user, password, database;
                    Array arrayConnecion = this.objFunc.LoadXmlConfig(fileNameXml);
                    serverAddress = arrayConnecion.GetValue(0).ToString();
                    database = arrayConnecion.GetValue(1).ToString();
                    isAuthentication = arrayConnecion.GetValue(2).ToString();
                    if (isAuthentication == "yes")
                    {
                        authentication = "Trusted_Connection=yes";
                    }
                    else
                    {
                        user = arrayConnecion.GetValue(3).ToString();
                        password = arrayConnecion.GetValue(4).ToString();
                        authentication = "Uid=" + user + ";Pwd=" + password + ";";
                    }
                    string connectionString = "Driver={SQL Native Client};Server=" + serverAddress + ";Database=" + database + ";" + authentication;
                    try
                    {
                        this.objCon.open(connectionString);
                        if (this.objCon.connected() == ConnectionState.Open)
                        {
                            this.banco = database;

                            Thread threadCheckDBSQLServer = new Thread(new ThreadStart(this.checkDB));
                            threadCheckDBSQLServer.IsBackground = true;
                            threadCheckDBSQLServer.Name = "CheckDB SQL Server";
                            threadCheckDBSQLServer.Start();

                            threadCheckDBSQLServer.Join();

                            if (!this.dbcorrupted)
                            {
                                Thread threadGerarXmlSQLServer = new Thread(new ThreadStart(this.gerarXML));
                                threadGerarXmlSQLServer.IsBackground = true;
                                threadGerarXmlSQLServer.Name = "GerarXML";
                                threadGerarXmlSQLServer.Start();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Falha na conexão!");
                    }
                }
                else if (this.tipoBanco == "Firebird")
                {
                    String authentication, user, password, database;
                    Array arrayConnecion = this.objFunc.LoadXmlConfig(fileNameXml);
                    database = arrayConnecion.GetValue(5).ToString();
                    user = arrayConnecion.GetValue(6).ToString();
                    password = arrayConnecion.GetValue(7).ToString();
                    authentication = "Uid=" + user + ";Pwd=" + password;

                    string connectionString = "Driver=Firebird/InterBase(r) driver;" + authentication + ";DbName=" + database + ";";
                    try
                    {
                        this.objCon.open(connectionString);

                        if (this.objCon.connected() == ConnectionState.Open)
                        {
                            Thread threadGerarXmlFirebird = new Thread(new ThreadStart(this.gerarXML));
                            threadGerarXmlFirebird.IsBackground = true;
                            threadGerarXmlFirebird.Name = "GerarXMl";
                            threadGerarXmlFirebird.Start();
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Falha na conexão!");
                    }
                }

            }
        }
    }
}
