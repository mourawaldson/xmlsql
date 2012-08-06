using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace XMLSQL
{
    class Functions
    {
        string sqlserver_server, sqlserver_database, sqlserver_windows_authentication, sqlserver_user, sqlserver_password;
        string firebird_filename, firebird_database, firebird_user, firebird_password;
        string txt_filename;
        string exec_hora;
        string[] array = new string[0];

        public string[] LoadXmlConfig(String xmlFile, String strAccess)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);

            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/config");

            foreach (XmlNode node in nodes)
            {

                if (strAccess == "sqlserver")
                {

                    //SQL Server
                    this.sqlserver_server = node["sqlserver_server"].InnerText;
                    this.sqlserver_database = node["sqlserver_database"].InnerText;
                    this.sqlserver_windows_authentication = node["sqlserver_windows_authentication"].InnerText;
                    this.sqlserver_user = node["sqlserver_user"].InnerText;
                    this.sqlserver_password = node["sqlserver_password"].InnerText;

                    this.array = new string[5];
                    this.array[0] = this.sqlserver_server;
                    this.array[1] = this.sqlserver_database;
                    this.array[2] = this.sqlserver_windows_authentication;
                    this.array[3] = this.sqlserver_user;
                    this.array[4] = this.sqlserver_password;

                    return this.array;

                }
                else if (strAccess == "firebird")
                {
                    //FireBird
                    this.firebird_filename = node["firebird_filename"].InnerText;
                    this.firebird_user = node["firebird_user"].InnerText;
                    this.firebird_password = node["firebird_password"].InnerText;

                    this.array = new string[3];
                    this.array[0] = this.firebird_filename;
                    this.array[1] = this.firebird_user;
                    this.array[2] = this.firebird_password;

                    return this.array;
                }
                else if (strAccess == "txt")
                {
                    //TXT
                    this.txt_filename = node["txt_filename"].InnerText;

                    this.array = new string[1];
                    this.array[0] = this.txt_filename;

                    return this.array;
                }
                else if (strAccess == "hora")
                {
                    //Hora Inicial
                    this.exec_hora = node["exec_hora"].InnerText;

                    this.array = new string[1];
                    this.array[0] = this.exec_hora;

                    return this.array;
                }
                return array;
            }
            return array;
        }

        public string[] LoadXmlConfig(String xmlFile)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);

            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/config");

            foreach (XmlNode node in nodes)
            {
                //SQL Server
                this.sqlserver_server = node["sqlserver_server"].InnerText;
                this.sqlserver_database = node["sqlserver_database"].InnerText;
                this.sqlserver_windows_authentication = node["sqlserver_windows_authentication"].InnerText;
                this.sqlserver_user = node["sqlserver_user"].InnerText;
                this.sqlserver_password = node["sqlserver_password"].InnerText;

                //FireBird
                this.firebird_filename = node["firebird_filename"].InnerText;
                this.firebird_user = node["firebird_user"].InnerText;
                this.firebird_password = node["firebird_password"].InnerText;

                //TXT
                this.txt_filename = node["txt_filename"].InnerText;

                //HORA
                this.exec_hora = node["exec_hora"].InnerText;

                this.array = new string[10];
                this.array[0] = this.sqlserver_server;
                this.array[1] = this.sqlserver_database;
                this.array[2] = this.sqlserver_windows_authentication;
                this.array[3] = this.sqlserver_user;
                this.array[4] = this.sqlserver_password;
                this.array[5] = this.firebird_filename;
                this.array[6] = this.firebird_user;
                this.array[7] = this.firebird_password;
                this.array[8] = this.txt_filename;
                this.array[9] = this.exec_hora;

                return this.array;
            }
            return array;
        }
    }
}
