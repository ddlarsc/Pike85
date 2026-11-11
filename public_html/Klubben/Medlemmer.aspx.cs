using System;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Klubben
{
    /// <summary>
    /// Summary description for Medlemmer.
    /// </summary>
    public partial class Medlemmer : SkabelonPage
    {
        private MySqlConnection _connection;
        private MySqlCommand _command;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HentMedlemsdata();
            }
        }

        private void HentMedlemsdata()
        {
            MySqlDataReader reader = null;
            try
            {
                _connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                _connection.Open();
                _command = new MySqlCommand("SELECT `Navn`, `Foedselsdag`, `Beskrivelse`, `Email`, `Email2`, `Adresse`, `By`, `PostNr`, `HjemTlf`, `ArbTlf`, `MobilTlf`, `Billed`, `Indmeldt` FROM bruger WHERE Offentlig='1' ORDER BY Navn", _connection);
                reader = _command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        do
                        {
                            medlemmer.InnerHtml += "<p align='left'><BR/><hr color='#000366'>";

                            DateTime dt = DateTime.Parse(reader["Foedselsdag"].ToString());
                            DateTime dtIndmeldt = DateTime.Parse(reader["Indmeldt"].ToString());

                            medlemmer.InnerHtml += "<table border='0' width='100%' cellspacing='0' cellpadding='2'><tr><td width=100>";
                            medlemmer.InnerHtml += "<tr><td rowspan='2' width='100'>";

                            if (reader["Billed"].ToString() != String.Empty)
                            {
                                medlemmer.InnerHtml += "<IMG border='0' alt='' src='/Billeder/Medlemmer/" + reader["Billed"] + ".gif'>";
                            }
                            medlemmer.InnerHtml += "</td><td>";

                            medlemmer.InnerHtml += "<B>" + reader["Navn"] + "</B>";
                            medlemmer.InnerHtml += "<BR/>Fødseldsdag : " + dt.ToShortDateString();
                            medlemmer.InnerHtml += "<BR/>Indmeldt : " + dtIndmeldt.Year.ToString(CultureInfo.InvariantCulture);
                            medlemmer.InnerHtml += "</td></tr><tr><td>";

                            if (reader["Beskrivelse"].ToString() != String.Empty)
                            {
                                medlemmer.InnerHtml += reader["Beskrivelse"].ToString().Replace("\r", "<BR>");
                            }

                            if (Page.User.Identity.IsAuthenticated)
                            {
                                if (reader["Email"].ToString() != String.Empty)
                                {
                                    medlemmer.InnerHtml += "<P>Email adresse : <A HREF='mailto:" + reader["Email"] + "'>" + reader["Email"] + "</A></P>";
                                }

                                if (reader["Email2"].ToString() != String.Empty)
                                {
                                    medlemmer.InnerHtml += "Email2 adresse : <A HREF='mailto:" + reader["Email2"] + "'>" + reader["Email2"] + "</A><BR>";
                                }

                                if (reader["Adresse"].ToString() != String.Empty)
                                {
                                    medlemmer.InnerHtml += "Adresse : <B>" + reader["Adresse"] + "</B><BR>";
                                }

                                if (reader["PostNr"].ToString() != String.Empty)
                                {
                                    medlemmer.InnerHtml += "Post Nr.: <B>" + reader["PostNr"] + "</B><BR>";
                                }
                                if (reader["By"].ToString() != String.Empty)
                                {
                                    medlemmer.InnerHtml += "By : <B>" + reader["By"] + "</B><BR>";
                                }

                                if (reader["HjemTlf"].ToString() != String.Empty)
                                {
                                    medlemmer.InnerHtml += "Hjemme Tlf : <B>" + reader["HjemTlf"] + "</B><BR>";
                                }
                                if (reader["MobilTlf"].ToString() != String.Empty)
                                {
                                    medlemmer.InnerHtml += "Mobil Tlf : <B>" + reader["MobilTlf"] + "</B><BR>";
                                }
                                if (reader["ArbTlf"].ToString() != String.Empty)
                                {
                                    medlemmer.InnerHtml += "Arbejds Tlf : <B>" + reader["ArbTlf"] + "</B><BR>";
                                }
                            }

                            medlemmer.InnerHtml += "</td></tr></table>";
                        }
                        while (reader.Read());
                    }
                }

                reader.Close();
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
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

        }
        #endregion
    }
}
