using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Medlemmer
{
    /// <summary>
    /// Summary description for Rediger_Opslag.
    /// </summary>
    public partial class RedigerOpslag : SkabelonPage
    {
        private string _commandText = string.Empty;

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
                Image1.Visible = false;

                if (Request.QueryString.Get("OpslagId") != "")
                {
                    MySqlDataReader reader = null;
                    try
                    {
                        _commandText = "select * from opslag where OpslagId='" + Request.QueryString.Get("OpslagId") + "'";
                        _connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                        _connection.Open();
                        _command = new MySqlCommand(_commandText, _connection);
                        reader = _command.ExecuteReader(CommandBehavior.CloseConnection);

                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                DateTime dt = DateTime.Parse(reader["Dato"].ToString());
                                Kalender_Dato.TodaysDate = dt.Date;
                                Kalender_Dato.SelectedDate = dt.Date;

                                TextBox_Overskrift.Text = reader["Overskrift"].ToString();
                                TextBox_Beskrivelse.Text = reader["Beskrivelse"].ToString();
                                TextBox_Link.Text = reader["Link"].ToString();

                                CheckBox_Offentlig.Checked = Convert.ToBoolean(Convert.ToInt32(reader["Offentlig"].ToString()));

                                if (reader["Billed"].ToString() != "")
                                {
                                    Image1.Visible = true;
                                    Image1.ImageUrl = reader["BilledMappe"] + reader["Billed"].ToString() + "_thumb.jpg";
                                }
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
                else
                {
                    Label_Output.Text = "Opslaget kunne ikke hentes, kontakt webmasteren.";
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


        protected void ButtonUploadClick(object sender, EventArgs e)
        {
            string filnavn1 = String.Empty;
            string billedMappe = "/Billeder/Opslag/" + Kalender_Dato.SelectedDate.Year.ToString(CultureInfo.InvariantCulture) + "/";

            try
            {
                if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength != 0)
                {
                    filnavn1 = InternalHelper.OpretBilledFiler(Page.Server.MapPath(billedMappe), FileUpload1, RadioButton_Billed1_Staaende.Checked);
                }

                _connection = new MySqlConnection(Application["MySQLConnString"].ToString());

                string commandText = "UPDATE opslag SET " +
                  "Forfatter = '" + User.Identity.Name + "', " +
                  "Dato = '" + Kalender_Dato.SelectedDate.Year.ToString(CultureInfo.InvariantCulture) + "-" + Kalender_Dato.SelectedDate.Month.ToString(CultureInfo.InvariantCulture) + "-" + Kalender_Dato.SelectedDate.Day.ToString(CultureInfo.InvariantCulture) + "', " +
                  "Overskrift = '" + TextBox_Overskrift.Text + "', " +
                  "Beskrivelse = '" + TextBox_Beskrivelse.Text + "', " +
                  "Link = '" + InternalHelper.CheckLinkAddress(TextBox_Link.Text) + "', " +
                  "BilledMappe = '" + billedMappe + "', ";

                if (filnavn1 != string.Empty)
                {
                    commandText += "Billed = '" + filnavn1 + "', ";
                }

                commandText += "Offentlig = '" + Convert.ToByte(CheckBox_Offentlig.Checked) + "'" +
                  " WHERE OpslagId = '" + Request.QueryString.Get("OpslagId") + "'";

                _command = new MySqlCommand(commandText, _connection);
                _connection.Open();
                _command.ExecuteNonQuery();

                Label_Output.ForeColor = Color.Blue;
                Label_Output.Text = "Opslaget er gemt i databasen !!!";
            }
            catch
            {
                Label_Output.ForeColor = Color.Red;
                Label_Output.Text = "Det er sket en fejl, prøv igen";
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

    }
}
