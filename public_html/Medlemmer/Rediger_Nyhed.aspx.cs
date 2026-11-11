using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Medlemmer
{
    /// <summary>
    /// Summary description for Rediger_Nyhed.
    /// </summary>
    public partial class RedigerNyhed : SkabelonPage
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
                Image2.Visible = false;
                Image3.Visible = false;

                if (Request.QueryString.Get("Id") != "")
                {
                    MySqlDataReader reader = null;
                    try
                    {
                        _commandText = "select * from nyheder where Id='" + Request.QueryString.Get("Id") + "'";
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

                                if (reader["Billed1"].ToString() != "")
                                {
                                    Image1.Visible = true;
                                    Image1.ImageUrl = reader["BilledMappe"] + reader["Billed1"].ToString() + "_thumb.jpg";
                                }

                                if (reader["Billed2"].ToString() != "")
                                {
                                    Image2.Visible = true;
                                    Image2.ImageUrl = reader["BilledMappe"] + reader["Billed2"].ToString() + "_thumb.jpg";
                                }

                                if (reader["Billed3"].ToString() != "")
                                {
                                    Image3.Visible = true;
                                    Image3.ImageUrl = reader["BilledMappe"] + reader["Billed3"].ToString() + "_thumb.jpg";
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
                    Label_Output.Text = "Nyheden kunne ikke hentes, kontakt webmasteren.";
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

                string commandText = "UPDATE nyheder SET " +
                  "Forfatter = '" + User.Identity.Name + "', " +
                  "Dato = '" + Kalender_Dato.SelectedDate.Year.ToString(CultureInfo.InvariantCulture) + "-" + Kalender_Dato.SelectedDate.Month.ToString(CultureInfo.InvariantCulture) + "-" + Kalender_Dato.SelectedDate.Day.ToString(CultureInfo.InvariantCulture) + "', " +
                  "Overskrift = '" + TextBox_Overskrift.Text + "', " +
                  "Beskrivelse = '" + TextBox_Beskrivelse.Text + "', " +
                  "Link = '" + InternalHelper.CheckLinkAddress(TextBox_Link.Text) + "', " +
                  "BilledMappe = '" + billedMappe + "', ";

                if (filnavn1 != string.Empty)
                {
                    commandText += "Billed1 = '" + filnavn1 + "', ";
                }

                if (filnavn2 != string.Empty)
                {
                    commandText += "Billed2 = '" + filnavn2 + "', ";
                }

                if (filnavn3 != string.Empty)
                {
                    commandText += "Billed3 = '" + filnavn3 + "', ";
                }

                commandText += "Offentlig = '" + Convert.ToByte(CheckBox_Offentlig.Checked) + "'" +
                  " WHERE Id = '" + Request.QueryString.Get("Id") + "'";

                _command = new MySqlCommand(commandText, _connection);
                _connection.Open();
                _command.ExecuteNonQuery();

                Label_Output.ForeColor = Color.Blue;
                Label_Output.Text = "Nyheden er gemt i databasen !!!";
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
