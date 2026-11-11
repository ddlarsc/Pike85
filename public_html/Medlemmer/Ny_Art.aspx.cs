using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Medlemmer
{
    /// <summary>
    /// Summary description for Ny_Art.
    /// </summary>
    public partial class NyArt : SkabelonPage
    {
        private MySqlCommand _command;
        private MySqlConnection _connection;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsStartupScriptRegistered("Focus"))
            {
                InternalHelper.SetFocus(TextBox_ArtNavn);
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

        protected void ButtonOpretArtClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (CheckArt(TextBox_ArtNavn.Text))
                {
                    Label_Status.ForeColor = System.Drawing.Color.Red;
                    Label_Status.Text = "Arten findes allerede i databasen, prøv igen";
                }
                else
                {
                    if (TilfojArt(TextBox_ArtNavn.Text))
                    {
                        Label_Status.ForeColor = System.Drawing.Color.Blue;
                        Label_Status.Text = "Arten er tilføjet databasen";
                    }
                    else
                    {
                        Label_Status.ForeColor = System.Drawing.Color.Red;
                        Label_Status.Text = "Der er sket en fejl prøv igen";
                    }
                }

                TextBox_ArtNavn.Text = String.Empty;
            }
        }

        private bool TilfojArt(string art)
        {
            bool success = false;
            try
            {
                _connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                string sqlText = "INSERT INTO arter (ArtId, Navn) " +
                                  "VALUES(NULL, '" + art + "')";
                _command = new MySqlCommand(sqlText, _connection);

                _connection.Open();
                if (_command.ExecuteNonQuery() != 0)
                {
                    success = true;
                }
            }
            catch
            {
                success = false;
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return success;
        }

        private bool CheckArt(string art)
        {
            bool success = false;
            try
            {
                _connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                string sqlText = "SELECT Navn FROM arter WHERE ArtNavn='" + art + "'";
                _command = new MySqlCommand(sqlText, _connection);

                _connection.Open();
                Label_Status.ForeColor = System.Drawing.Color.Red;
                if (_command.ExecuteScalar().ToString().ToUpper() == art.ToUpper())
                {
                    success = true;
                }
            }
            catch
            {
                success = false;
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return success;
        }
    }
}
