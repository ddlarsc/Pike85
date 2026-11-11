using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;

namespace Pike85
{
    /// <summary>
    /// Summary description for Opslag.
    /// </summary>
    public partial class Opslag : System.Web.UI.Page
    {
        protected MySqlConnection connection;
        protected MySqlCommand command;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                HentOpslag();
            }
        }

        private void HentOpslag()
        {
            MySqlDataReader reader = null;
            try
            {
                this.connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                this.connection.Open();
                if (User.Identity.IsAuthenticated)
                {
                    this.command = new MySqlCommand("SELECT OpslagId, Forfatter, Dato, Overskrift, Beskrivelse, Link, BilledMappe, Billed FROM opslag ORDER BY Dato DESC LIMIT 0, 10", connection);
                }
                else
                {
                    this.command = new MySqlCommand("SELECT OpslagId, Forfatter, Dato, Overskrift, Beskrivelse, Link, BilledMappe, Billed FROM opslag WHERE Offentlig='1' ORDER BY Dato DESC LIMIT 0, 10", connection);
                }

                reader = this.command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    if (reader.Read() == true)
                    {
                        DateTime dt;
                        do
                        {
                            dt = DateTime.Parse(reader["Dato"].ToString());
                            opslag.InnerHtml += "<P><B><A NAME='" + reader["OpslagId"].ToString() + "'>" + dt.ToShortDateString() + " " + reader["Overskrift"].ToString() + "</A></B>";

                            if (reader["Beskrivelse"].ToString() != String.Empty)
                            {
                                opslag.InnerHtml += "<P>" + reader["Beskrivelse"].ToString().Replace("\r", "<BR>");
                            }

                            if (reader["Link"].ToString() != String.Empty)
                            {
                                opslag.InnerHtml += "<P>Se mere her : <A HREF='http://" + reader["Link"].ToString() + "' target=_blank>http://" + reader["Link"].ToString() + "</A>";
                            }

                            if (reader["Billed"].ToString() != String.Empty)
                            {
                                opslag.InnerHtml += "<P><A HREF='" + reader["BilledMappe"].ToString() + reader["Billed"].ToString() + ".jpg' target=_blank>";
                                opslag.InnerHtml += "<IMG border='0' alt='' src='" + reader["BilledMappe"].ToString() + reader["Billed"].ToString() + "_thumb.jpg'>";
                                opslag.InnerHtml += "</A>";
                            }

                            opslag.InnerHtml += "<BR/><hr color='#000366'>";
                        }
                        while (reader.Read() == true);
                    }
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ID = "Opslag";
        }

        #endregion
    }
}
