using System;
using System.Data;
using System.Drawing;
using System.Web.Security;
using MySql.Data.MySqlClient;

namespace Medlemmer
{
    /// <summary>
    /// Summary description for Skift_Kodeord.
    /// </summary>
    public partial class SkiftKodeord : SkabelonPage
    {
        private MySqlCommand _command;
        private MySqlConnection _connection;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsStartupScriptRegistered("Focus"))
            {
                InternalHelper.SetFocus(Textbox_GamleKodeord);
            }

            if (!IsPostBack)
            {
                if (User.IsInRole("Administrator"))
                {
                    DropDownList_BrugerId.Visible = true;
                    Requiredfieldvalidator3.Enabled = false;
                    Label_Bruger.Visible = false;
                    const string commandText = "SELECT BrugerId FROM bruger ORDER BY Navn ASC";
                    if (!InternalHelper.DropDownListReader(Application["MySQLConnString"].ToString(), commandText, DropDownList_BrugerId, "BrugerId"))
                    {
                        Label_Status.ForeColor = Color.Red;
                        Label_Status.Text = "Medlemmer kunne ikke hentes, kontakt webmasteren";
                    }
                }
                else
                {
                    DropDownList_BrugerId.Visible = false;
                    Label_Bruger.Visible = true;
                    Label_Bruger.Text = User.Identity.Name;
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

        protected void ButtonSkiftKodeordClick(object sender, EventArgs e)
        {
            if (Page.IsValid && User.Identity.IsAuthenticated)
            {
                if (!User.IsInRole("Administrator"))
                {
                    if (!CheckKodeord(Textbox_GamleKodeord.Text))
                    {
                        Label_Status.ForeColor = Color.Red;
                        Label_Status.Text = "Gamle kodeord er ikke korrekt, prøv igen";
                        return;
                    }
                }

                if (SkiftKode(TextBox_NytKodeord.Text))
                {
                    Label_Status.ForeColor = Color.Blue;
                    if (User.IsInRole("Administrator"))
                    {
                        Label_Status.Text = "Kodeord er opdateret for brugerid : " + DropDownList_BrugerId.SelectedItem.Value;
                    }
                    else
                    {
                        Label_Status.Text = "Kodeord er opdateret for brugerid : " + User.Identity.Name;
                    }

                    FormsAuthentication.SignOut();
                }
                else
                {
                    Label_Status.ForeColor = Color.Red;
                    Label_Status.Text = "Der er sket en fejl prøv igen";
                }

                Textbox_GamleKodeord.Text = String.Empty;
                TextBox_NytKodeord.Text = String.Empty;
                TextBox_NytKodeordIgen.Text = String.Empty;
            }
        }

        private bool CheckKodeord(string kodeord)
        {
            bool success = false;
            MySqlDataReader readerBruger = null;
            try
            {
                _connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                _connection.Open();
                if (User.IsInRole("Administrator"))
                {
                    _command = new MySqlCommand("SELECT * FROM bruger WHERE BrugerId='" + DropDownList_BrugerId.SelectedItem.Value + "'", _connection);
                }
                else
                {
                    _command = new MySqlCommand("SELECT * FROM bruger WHERE BrugerId='" + User.Identity.Name + "'", _connection);
                }

                readerBruger = _command.ExecuteReader(CommandBehavior.CloseConnection);
                while (readerBruger.Read())
                {
                    if (FormsAuthentication.HashPasswordForStoringInConfigFile(kodeord, "SHA1") == readerBruger["Kodeord"].ToString())
                    {
                        success = true;
                    }
                }

                readerBruger.Close();
            }
            catch
            {
                success = false;
            }
            finally
            {
                if (readerBruger != null && !readerBruger.IsClosed)
                {
                    readerBruger.Close();
                }

                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }

            }

            return success;
        }

        private bool SkiftKode(string nytKodeord)
        {
            bool success = false;

            string sqlText;
            if (User.IsInRole("Administrator"))
            {
                sqlText = "UPDATE bruger SET Kodeord='" +
                  FormsAuthentication.HashPasswordForStoringInConfigFile(nytKodeord, "SHA1") +
                  "' WHERE BrugerId='" + DropDownList_BrugerId.SelectedItem.Value + "'";
            }
            else
            {
                sqlText = "UPDATE bruger SET Kodeord='" +
                  FormsAuthentication.HashPasswordForStoringInConfigFile(nytKodeord, "SHA1") +
                  "' WHERE BrugerId='" + User.Identity.Name + "'";

            }
            try
            {
                _connection = new MySqlConnection(Application["MySQLConnString"].ToString());
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
    }
}
