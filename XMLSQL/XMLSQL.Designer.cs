namespace XMLSQL
{
    partial class XMLSQL
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMLSQL));
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbTipoBanco = new System.Windows.Forms.ComboBox();
            this.lbTipoBanco = new System.Windows.Forms.Label();
            this.btnGerarXmlTxt = new System.Windows.Forms.Button();
            this.timerXMLSQL = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(41, 52);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(76, 23);
            this.btnIniciar.TabIndex = 18;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnConnectSQLServer_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(41, 52);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnDisconnectSQLServer_Click);
            // 
            // cbTipoBanco
            // 
            this.cbTipoBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoBanco.FormattingEnabled = true;
            this.cbTipoBanco.Items.AddRange(new object[] {
            "SQL Server",
            "Firebird",
            "Arquivo .txt"});
            this.cbTipoBanco.Location = new System.Drawing.Point(34, 26);
            this.cbTipoBanco.Name = "cbTipoBanco";
            this.cbTipoBanco.Size = new System.Drawing.Size(97, 21);
            this.cbTipoBanco.TabIndex = 20;
            this.cbTipoBanco.SelectedIndexChanged += new System.EventHandler(this.cbTipoBanco_SelectedIndexChanged);
            // 
            // lbTipoBanco
            // 
            this.lbTipoBanco.AutoSize = true;
            this.lbTipoBanco.Location = new System.Drawing.Point(39, 9);
            this.lbTipoBanco.Name = "lbTipoBanco";
            this.lbTipoBanco.Size = new System.Drawing.Size(77, 13);
            this.lbTipoBanco.TabIndex = 21;
            this.lbTipoBanco.Text = "Tipo de Banco";
            // 
            // btnGerarXmlTxt
            // 
            this.btnGerarXmlTxt.Location = new System.Drawing.Point(41, 52);
            this.btnGerarXmlTxt.Name = "btnGerarXmlTxt";
            this.btnGerarXmlTxt.Size = new System.Drawing.Size(76, 23);
            this.btnGerarXmlTxt.TabIndex = 3;
            this.btnGerarXmlTxt.Text = "Gerar Xml";
            this.btnGerarXmlTxt.UseVisualStyleBackColor = true;
            // 
            // timerXMLSQL
            // 
            this.timerXMLSQL.Interval = 1000;
            this.timerXMLSQL.Tick += new System.EventHandler(this.timerXMLSQL_Tick);
            // 
            // XMLSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(162, 90);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGerarXmlTxt);
            this.Controls.Add(this.lbTipoBanco);
            this.Controls.Add(this.cbTipoBanco);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "XMLSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XMLSQL";
            this.Load += new System.EventHandler(this.XMLSQL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbTipoBanco;
        private System.Windows.Forms.Label lbTipoBanco;
        private System.Windows.Forms.Button btnGerarXmlTxt;
        private System.Windows.Forms.Timer timerXMLSQL;

    }
}

