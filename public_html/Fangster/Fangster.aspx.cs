using System;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Fangster
{
    /// <summary>
    /// Summary description for Fangster.
    /// </summary>
    public partial class Fangster : SkabelonPage
    {
        const int NumberOfRows = 25;

        protected MySqlConnection Connection;
        protected MySqlCommand Command;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["FraFangstNr"] = 0;
                ViewState["TilFangstNr"] = 25;
                ViewState["TotalAntalFangster"] = HentAntalFangster() + 1;
                if ((int)ViewState["TotalAntalFangster"] > (int)ViewState["TilFangstNr"])
                {
                    LinkButton_Naeste_Top.Visible = true;
                    LinkButton_Naeste_Bund.Visible = true;
                }
                LinkButton_Forrige_Top.Visible = false;
                LinkButton_Forrige_Bund.Visible = false;
                HentFangster();
            }
        }

        private int HentAntalFangster()
        {
            int antal;
            Connection = new MySqlConnection(Application["MySQLConnString"].ToString());
            try
            {
                Connection.Open();
                Command = User.Identity.IsAuthenticated ? new MySqlCommand("SELECT COUNT(*) FROM fangster", Connection) : new MySqlCommand("SELECT COUNT(*) FROM fangster WHERE Offentlig='1'", Connection);

                antal = Int32.Parse(Command.ExecuteScalar().ToString());
            }
            finally
            {
                if (Connection != null && Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return antal;
        }

        private void HentFangster()
        {
            MySqlDataReader reader = null;
            try
            {
                Connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                Connection.Open();
                Command = User.Identity.IsAuthenticated ? new MySqlCommand("SELECT * FROM fangster ORDER BY Dato DESC LIMIT " + ViewState["FraFangstNr"] + ", " + NumberOfRows.ToString(CultureInfo.InvariantCulture), Connection) : new MySqlCommand("SELECT * FROM fangster WHERE Offentlig='1' ORDER BY Dato DESC LIMIT " + ViewState["FraFangstNr"] + ", " + NumberOfRows.ToString(CultureInfo.InvariantCulture), Connection);

                reader = Command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        fangster.InnerHtml = string.Empty;
                        do
                        {
                            DateTime dt = DateTime.Parse(reader["Dato"].ToString());
                            fangster.InnerHtml += "<P><B><A NAME='" + reader["FangstId"] + "'>" + dt.ToShortDateString();
                            fangster.InnerHtml += " - " + reader["Art"] + " " + reader["Vaegt"];
                            fangster.InnerHtml += " - fanget af " + reader["Fanger"] + "</A></B>";

                            if (reader["Redskab"].ToString() != String.Empty)
                            {
                                fangster.InnerHtml += "<P>Fisken er fanget på dette redskab : <B>" + reader["Redskab"] + "</B>";
                            }

                            if (reader["Fiskevand"].ToString() != String.Empty)
                            {
                                fangster.InnerHtml += "<P>Fiskevand : <B>" + reader["Fiskevand"];
                                if (reader["Placering"].ToString() != String.Empty)
                                {
                                    fangster.InnerHtml += " - " + reader["Placering"] + "</B>";
                                }
                                else
                                {
                                    fangster.InnerHtml += "</B>";
                                }
                            }

                            if (reader["Land"].ToString() != String.Empty)
                            {
                                fangster.InnerHtml += "<P>Land : <B>" + reader["Land"] + "</B>";
                            }

                            if (reader["Vejr"].ToString() != String.Empty)
                            {
                                fangster.InnerHtml += "<P>Vejr : <B>" + reader["Vejr"] + "</B>";
                            }

                            if (reader["Beskrivelse"].ToString() != String.Empty)
                            {
                                fangster.InnerHtml += "<P>Beskrivelse : <BR/><B>" + reader["Beskrivelse"].ToString().Replace("\r", "<BR>") + "</B>";
                            }

                            if (reader["Link"].ToString() != String.Empty)
                            {
                                fangster.InnerHtml += "<P>Se mere her : <A HREF='http://" + reader["Link"] + "' target=_blank>http://" + reader["Link"] + "</A>";
                            }
                            if (reader["Billed1"].ToString() != String.Empty)
                            {
                                fangster.InnerHtml += "<P><A HREF=javascript:PopupBilled('" + reader["BilledMappe"] + reader["Billed1"] + ".jpg')>";
                                fangster.InnerHtml += "<IMG border='0' alt='' src='" + reader["BilledMappe"] + reader["Billed1"] + "_thumb.jpg'>";
                                fangster.InnerHtml += "</A>";
                            }

                            if (reader["Billed2"].ToString() != String.Empty)
                            {
                                fangster.InnerHtml += "  <A HREF=javascript:PopupBilled('" + reader["BilledMappe"] + reader["Billed2"] + ".jpg')>";
                                fangster.InnerHtml += "<IMG border='0' alt='' src='" + reader["BilledMappe"] + reader["Billed2"] + "_thumb.jpg'>";
                                fangster.InnerHtml += "</A>";
                            }

                            if (reader["Billed3"].ToString() != String.Empty)
                            {
                                fangster.InnerHtml += "  <A HREF=javascript:PopupBilled('" + reader["BilledMappe"] + reader["Billed3"] + ".jpg')>";
                                fangster.InnerHtml += "<IMG border='0' alt='' src='" + reader["BilledMappe"] + reader["Billed3"] + "_thumb.jpg'>";
                                fangster.InnerHtml += "</A>";
                            }
                            fangster.InnerHtml += "<BR/><hr color='#000366'>";
                        }
                        while (reader.Read());
                    }
                }

                reader.Close();
            }
            catch
            {
                Label_Output.Text = "Der er sket en fejl fangsterne kunne ikke hentes.";
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (Connection != null && Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        //#region Web Form Designer generated code
        //override protected void OnInit(EventArgs e)
        //{
        //    //
        //    // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //    //
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>
        //private void InitializeComponent()
        //{
        //    this.ID = "nye";

        //}
        //#endregion

        protected void LinkButtonForrigeTopClick(object sender, EventArgs e)
        {
            ViewState["TilFangstNr"] = (int)ViewState["FraFangstNr"];
            ViewState["FraFangstNr"] = (int)ViewState["FraFangstNr"] - NumberOfRows;
            if ((int)ViewState["FraFangstNr"] == 0)
            {
                LinkButton_Forrige_Top.Visible = false;
                LinkButton_Forrige_Bund.Visible = false;
            }
            LinkButton_Naeste_Top.Visible = true;
            LinkButton_Naeste_Bund.Visible = true;
            HentFangster();
        }

        protected void LinkButtonNaesteTopClick(object sender, EventArgs e)
        {
            ViewState["FraFangstNr"] = (int)ViewState["TilFangstNr"];
            ViewState["TilFangstNr"] = (int)ViewState["TilFangstNr"] + NumberOfRows;
            if ((int)ViewState["TotalAntalFangster"] < (int)ViewState["TilFangstNr"])
            {
                LinkButton_Naeste_Top.Visible = false;
                LinkButton_Naeste_Bund.Visible = false;
            }
            LinkButton_Forrige_Top.Visible = true;
            LinkButton_Forrige_Bund.Visible = true;
            HentFangster();
        }

        protected void LinkButtonForrigeBundClick(object sender, EventArgs e)
        {
            ViewState["TilFangstNr"] = (int)ViewState["FraFangstNr"];
            ViewState["FraFangstNr"] = (int)ViewState["FraFangstNr"] - NumberOfRows;
            if ((int)ViewState["FraFangstNr"] == 0)
            {
                LinkButton_Forrige_Top.Visible = false;
                LinkButton_Forrige_Bund.Visible = false;
            }
            LinkButton_Naeste_Top.Visible = true;
            LinkButton_Naeste_Bund.Visible = true;
            HentFangster();
        }

        protected void LinkButtonNaesteBundClick(object sender, EventArgs e)
        {
            ViewState["FraFangstNr"] = (int)ViewState["TilFangstNr"];
            ViewState["TilFangstNr"] = (int)ViewState["TilFangstNr"] + NumberOfRows;
            if ((int)ViewState["TotalAntalFangster"] < (int)ViewState["TilFangstNr"])
            {
                LinkButton_Naeste_Top.Visible = false;
                LinkButton_Naeste_Bund.Visible = false;
            }
            LinkButton_Forrige_Top.Visible = true;
            LinkButton_Forrige_Bund.Visible = true;
            HentFangster();
        }
    }   
}
