using System;
using System.Globalization;
using System.Web.UI;
using Control;
using MySql.Data.MySqlClient;

namespace Skabelon
{
    public partial class SkabelonPike85 : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    HentBruger();                    
                    Calendar1.Visible = Context.User.Identity.IsAuthenticated;
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        private void HentBruger()
        {
            try
            {
                using (var connection = new MySqlConnection(Application["MySQLConnString"].ToString()))
                {
                    connection.Open();

                    var commandText = "SELECT distinct KortNavn FROM bruger WHERE Dead='0' AND MONTH(Foedselsdag)='" + DateTime.Today.Month.ToString(CultureInfo.InvariantCulture) + "' AND DAYOFMONTH(Foedselsdag)='" + DateTime.Today.Day.ToString(CultureInfo.InvariantCulture) + "'";
                    var command = new MySqlCommand(commandText, connection);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Label_Foedselar.Text = reader["KortNavn"].ToString();
                        Label_Foedselar.Visible = true;
                        Image_Flag.Visible = true;
                    }
                    else
                    {
                        Label_Foedselar.Visible = false;
                        Image_Flag.Visible = false;
                    }
                }
            }
            catch(Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
            }
        }
    }
}
