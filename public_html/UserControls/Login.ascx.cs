using System;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace UserControls
{
    /// <summary>
    ///		Summary description for Login.
    /// </summary>
    public partial class Login : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.ClientScript.IsStartupScriptRegistered("Focus"))
            {
                InternalHelper.SetFocus(TextBox_Login);
            }

            if (!IsPostBack)
            {
                TextBox_Kodeord.Attributes.Add("onkeypress", "return clickLoginButton(event,'" + LinkButton_Login.ClientID + "')");

                ViewState["Roller"] = String.Empty;

                if (Page.User.Identity.IsAuthenticated)
                {
                    Label_BrugerId.Visible = false;
                    Label_Kodeord.Visible = false;
                    TextBox_Login.Visible = false;
                    TextBox_Kodeord.Visible = false;
                    LinkButton_Login.Visible = false;
                    LinkButton_Logud.Visible = true;
                }
                else
                {
                    Label_BrugerId.Visible = true;
                    Label_Kodeord.Visible = true;
                    TextBox_Login.Visible = true;
                    TextBox_Kodeord.Visible = true;
                    LinkButton_Login.Visible = true;
                    LinkButton_Logud.Visible = false;
                }
            }
        }

        protected void LinkButtonLoginClick(object sender, EventArgs e)
        {
            if (CheckKodeord(TextBox_Login.Text, TextBox_Kodeord.Text))
            {
                // Opret authentication ticket
                var authTicket = new FormsAuthenticationTicket(
                      1,                                // version
                      TextBox_Login.Text,               // brugerid 
                      DateTime.Now,                     // oprettelse
                      DateTime.Now.AddMinutes(30),      // udløbstid i minutter
                      false,                            // persistent 
                      ViewState["Roller"].ToString());  // roller

                // Encrypt ticket.
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                // Opret cookie og tilføj encrypted ticket til cookie som data.
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                // Tilføj cookie til outgoing cookies collection.
                Response.Cookies.Add(authCookie);
                // Redirect brugeren til original requested side
                Response.Redirect(FormsAuthentication.GetRedirectUrl(TextBox_Login.Text, false));
            }
            else
            {
                Label_Fejl.ForeColor = Color.Red;
                Label_Fejl.Text = "FEJL, prøv igen...";
                InternalHelper.SetFocus(TextBox_Login);
            }
        }

        protected void LinkButtonLogudClick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/Default.aspx");
        }

        private bool CheckKodeord(string bruger, string kodeord)
        {
            try
            {
                using (var connection = new MySqlConnection(Application["MySQLConnString"].ToString()))
                {
                    connection.Open();
                    var command = new MySqlCommand("SELECT * FROM bruger WHERE BrugerId='" + bruger + "'", connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (FormsAuthentication.HashPasswordForStoringInConfigFile(kodeord, "SHA1") == reader["Kodeord"].ToString())
                            {
                                ViewState["Roller"] = reader["Roller"].ToString();
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
