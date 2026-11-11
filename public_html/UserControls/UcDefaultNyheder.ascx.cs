using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace UserControls
{
    public partial class UcDefaultNyheder:System.Web.UI.UserControl
    {
        private string _myConnection = string.Empty;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender,EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if(IsPostBack) { return; }

            HentNyheder();
        }

        private void HentNyheder()
        {
            MySqlConnection connection = null;
            MySqlDataReader reader = null;
            try
            {
                connection = new MySqlConnection(_myConnection);
                connection.Open();

                MySqlCommand command;
                if(Page.User.Identity.IsAuthenticated)
                {
                    command = new MySqlCommand("SELECT Id, Dato, Overskrift, Beskrivelse, Link, Billed1 FROM nyheder ORDER BY Dato DESC LIMIT 0, 5",connection);
                }
                else
                {
                    command = new MySqlCommand("SELECT Id, Dato, Overskrift, Beskrivelse, Link, Billed1, Offentlig FROM nyheder WHERE Offentlig='1' ORDER BY Dato DESC LIMIT 0, 5",connection);
                }

                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        DateTime dt = DateTime.Parse(reader["Dato"].ToString());
                        string beskrivelse = reader["Beskrivelse"].ToString();

                        spanNyheder.InnerHtml += "<B>" + dt.ToShortDateString() + " " + reader["Overskrift"] + "</B><BR/>";
                        if(beskrivelse != String.Empty)
                        {
                            if(beskrivelse.Length > 100)
                            {
                                spanNyheder.InnerHtml += beskrivelse.Substring(0,100) + " ... ";
                                spanNyheder.InnerHtml += "<A HREF='/Nyheder/Nyheder.aspx#" + reader["Id"];
                                spanNyheder.InnerHtml += "'> læs mere</A>.<BR/>";
                            }
                            else
                            {
                                spanNyheder.InnerHtml += beskrivelse;
                            }
                        }
                        spanNyheder.InnerHtml += "<hr width='95%'/>";
                    }
                }
            }
            finally
            {
                if(reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }

                if(connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
