using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Medlemmer
{
    /// <summary>
    /// Summary description for Indtast_Nyhed.
    /// </summary>
    public partial class IndtastNyhed : SkabelonPage
    {
        private MySqlConnection _connection;
        private MySqlCommand _command;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsStartupScriptRegistered("Focus"))
            {
                InternalHelper.SetFocus(TextBox_Overskrift);
            }

            if (!IsPostBack)
            {
                Kalender_Dato.SelectedDate = DateTime.Now;
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


        protected void ButtonUploadClick(object sender, EventArgs e)
        {
            string filnavn1 = String.Empty;
            string filnavn2 = String.Empty;
            string filnavn3 = String.Empty;
            string billedMappe = "/Billeder/Nyheder/" + Kalender_Dato.SelectedDate.Year.ToString(CultureInfo.InvariantCulture) + "/";

            try
            {
                if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength != 0)
                {
                    filnavn1 = InternalHelper.OpretBilledFiler(Page.Server.MapPath(billedMappe), FileUpload1, RadioButton_Billed1_Staaende.Checked);
                }

                if (FileUpload2.PostedFile != null && FileUpload2.PostedFile.ContentLength != 0)
                {
                    filnavn2 = InternalHelper.OpretBilledFiler(Page.Server.MapPath(billedMappe), FileUpload2, RadioButton_Billed2_Staaende.Checked);
                }

                if (FileUpload3.PostedFile != null && FileUpload3.PostedFile.ContentLength != 0)
                {
                    filnavn3 = InternalHelper.OpretBilledFiler(Page.Server.MapPath(billedMappe), FileUpload3, RadioButton_Billed3_Staaende.Checked);
                }

                _connection = new MySqlConnection(Application["MySQLConnString"].ToString());

                string sqlText = "INSERT INTO nyheder (Id, Forfatter, Dato, Overskrift, Beskrivelse, Link, BilledMappe, Billed1, Billed2, Billed3, Offentlig) " +
                  "VALUES(" +
                    // ID
                  "NULL, " +
                    // FORFATTER
                  "'" + User.Identity.Name + "', " +
                    // DATO
                  "'" + Kalender_Dato.SelectedDate.Year.ToString(CultureInfo.InvariantCulture) + "-" + Kalender_Dato.SelectedDate.Month.ToString(CultureInfo.InvariantCulture) + "-" + Kalender_Dato.SelectedDate.Day.ToString(CultureInfo.InvariantCulture) + "', " +
                    // OVERSKRIFT
                  "'" + TextBox_Overskrift.Text + "', " +
                    // BESKRIVELSE
                  "'" + TextBox_Beskrivelse.Text + "', " +
                    // LINK
                  "'" + InternalHelper.CheckLinkAddress(TextBox_Link.Text) + "', " +
                    // BILLEDMAPPE
                  "'" + billedMappe + "', " +
                    // BILLED1
                  "'" + filnavn1 + "', " +
                    // BILLED2
                  "'" + filnavn2 + "', " +
                    // BILLED3
                  "'" + filnavn3 + "', " +
                    // OFFENTLIG
                  "'" + Convert.ToByte(CheckBox_Offentlig.Checked) + "')";
                _command = new MySqlCommand(sqlText, _connection);
                _connection.Open();
                _command.ExecuteNonQuery();

                Label_Output.ForeColor = Color.Blue;
                Label_Output.Text = "Nyheden er gemt i databasen !!!";
            }
            catch (Exception ex)
            {
                Label_Output.ForeColor = Color.Red;
                Label_Output.Text = "Det er sket en fejl, prøv igen " + ex.Message;
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            TextBox_Overskrift.Text = String.Empty;
            TextBox_Beskrivelse.Text = String.Empty;
            TextBox_Link.Text = String.Empty;
            Kalender_Dato.SelectedDate = DateTime.Now;
            CheckBox_Offentlig.Checked = true;
        }

    }
}
