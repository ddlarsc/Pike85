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
    /// Summary description for Nyheder.
    /// </summary>
    public partial class Nyheder : System.Web.UI.Page
    {
        protected MySqlConnection connection;
        protected MySqlCommand command;

        protected System.Web.UI.WebControls.HyperLink HyperLink_Nyheder;
        protected System.Web.UI.WebControls.HyperLink HyperLink_Medlemmer;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                HentNyheder();
            }
        }

        private void HentNyheder()
        {
            MySqlDataReader reader = null;
            try
            {
                this.connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                this.connection.Open();
                if (User.Identity.IsAuthenticated)
                {
                    this.command = new MySqlCommand("SELECT * FROM nyheder ORDER BY Dato DESC LIMIT 0, 25", connection);
                }
                else
                {
                    this.command = new MySqlCommand("SELECT * FROM nyheder WHERE Offentlig='1' ORDER BY Dato DESC LIMIT 0, 25", connection);
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
                            nyheder.InnerHtml += "<P><B><A NAME='" + reader["Id"].ToString() + "'>" + dt.ToShortDateString() + " " + reader["Overskrift"].ToString() + "</A></B>";

                            if (reader["Beskrivelse"].ToString() != String.Empty)
                            {
                                nyheder.InnerHtml += "<P>" + reader["Beskrivelse"].ToString().Replace("\r", "<BR>");
                            }

                            if (reader["Link"].ToString() != String.Empty)
                            {
                                nyheder.InnerHtml += "<P>Se mere her : <A HREF='http://" + reader["Link"].ToString() + "' target=_blank>http://" + reader["Link"].ToString() + "</A>";
                            }

                            if (reader["Billed1"].ToString() != String.Empty)
                            {
                                nyheder.InnerHtml += "<P><A HREF=javascript:PopupBilled('" + reader["BilledMappe"].ToString() + reader["Billed1"].ToString() + ".jpg')>";
                                nyheder.InnerHtml += "<IMG border='0' alt='' src='" + reader["BilledMappe"].ToString() + reader["Billed1"].ToString() + "_thumb.jpg'>";
                                nyheder.InnerHtml += "</A>";
                            }

                            if (reader["Billed2"].ToString() != String.Empty)
                            {
                                nyheder.InnerHtml += " - <A HREF=javascript:PopupBilled('" + reader["BilledMappe"].ToString() + reader["Billed2"].ToString() + ".jpg')>";
                                nyheder.InnerHtml += "<IMG border='0' alt='' src='" + reader["BilledMappe"].ToString() + reader["Billed2"].ToString() + "_thumb.jpg'>";
                                nyheder.InnerHtml += "</A>";
                            }

                            if (reader["Billed3"].ToString() != String.Empty)
                            {
                                nyheder.InnerHtml += " - <A HREF=javascript:PopupBilled('" + reader["BilledMappe"].ToString() + reader["Billed3"].ToString() + ".jpg')>";
                                nyheder.InnerHtml += "<IMG border='0' alt='' src='" + reader["BilledMappe"].ToString() + reader["Billed3"].ToString() + "_thumb.jpg'>";
                                nyheder.InnerHtml += "</A>";
                            }

                            nyheder.InnerHtml += "<BR/><hr color='#000366'>";
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
            this.ID = "Nyheder";

        }
        #endregion
    }
}
