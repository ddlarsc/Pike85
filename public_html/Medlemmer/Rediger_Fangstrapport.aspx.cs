using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Medlemmer
{
    /// <summary>
    /// Summary description for Pike85_Template.
    /// </summary>
    public partial class RedigerFangstrapport : SkabelonPage
    {
        private string _commandText = string.Empty;
        private string _fangstId = string.Empty;
        private MySqlConnection _connection;
        private MySqlCommand _command;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated && Context.User.IsInRole("Administrator"))
            {
                CheckBox_YearFish.Visible = true;
            }
            else
            {
                CheckBox_YearFish.Visible = false;
            }

            _fangstId = Request.QueryString.Get("FangstId");
            if (!IsStartupScriptRegistered("Focus"))
            {
                InternalHelper.SetFocus(DropDownList_Dag);
            }

            if (!IsPostBack)
            {
                Image1.Visible = false;
                Image2.Visible = false;
                Image3.Visible = false;

                if (_fangstId != string.Empty)
                {
                    MySqlDataReader reader = null;
                    try
                    {
                        _commandText = "select * from fangster where FangstId='" + _fangstId + "'";
                        _connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                        _connection.Open();
                        _command = new MySqlCommand(_commandText, _connection);
                        reader = _command.ExecuteReader(CommandBehavior.CloseConnection);

                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                DateTime dt = DateTime.Parse(reader["Dato"].ToString());
                                TextBox_Aar.Text = dt.Year.ToString(CultureInfo.InvariantCulture);
                                DropDownList_Dag.SelectedIndex = dt.Day - 1;
                                DropDownList_Maaned.SelectedIndex = dt.Month - 1;

                                _commandText = "SELECT Navn FROM arter ORDER BY Navn ASC";
                                if (!InternalHelper.DropDownListReader(Application["MySQLConnString"].ToString(), _commandText, DropDownList_Art, "Navn"))
                                {
                                    Label_Output.ForeColor = Color.Red;
                                    Label_Output.Text = "Arterne kunne ikke hentes, kontakt webmasteren";
                                }

                                int i = 0;
                                foreach (ListItem item in DropDownList_Art.Items)
                                {
                                    if (item.Value == reader["Art"].ToString())
                                    {
                                        DropDownList_Art.SelectedIndex = i;
                                        break;
                                    }
                                    i++;
                                }

                                _commandText = "SELECT KortNavn FROM bruger WHERE Offentlig='1' ORDER BY KortNavn ASC";
                                if (!InternalHelper.DropDownListReader(Application["MySQLConnString"].ToString(), _commandText, DropDownList_Fanger, "KortNavn"))
                                {
                                    Label_Output.ForeColor = Color.Red;
                                    Label_Output.Text = "Fanger/medlem kunne ikke hentes, kontakt webmasteren";
                                }

                                i = 0;
                                foreach (ListItem item in DropDownList_Fanger.Items)
                                {
                                    if (item.Value == reader["Fanger"].ToString())
                                    {
                                        DropDownList_Fanger.SelectedIndex = i;
                                        break;
                                    }
                                    i++;
                                }

                                TextBox_Vaegt.Text = reader["Vaegt"].ToString();
                                TextBox_Laengde.Text = reader["Laengde"].ToString();
                                TextBox_Fiskevand.Text = reader["Fiskevand"].ToString();
                                TextBox_Placering.Text = reader["Placering"].ToString();
                                Textbox_Land.Text = reader["Land"].ToString();
                                TextBox_Redskab.Text = reader["Redskab"].ToString();
                                TextBox_Vejr.Text = reader["Vejr"].ToString();
                                TextBox_Beskrivelse.Text = reader["Beskrivelse"].ToString();
                                TextBox_Link.Text = reader["Link"].ToString();

                                CheckBox_Offentlig.Checked = Convert.ToBoolean(Convert.ToInt32(reader["Offentlig"].ToString()));
                                CheckBox_YearFish.Checked = Convert.ToBoolean(Convert.ToInt32(reader["YearFish"].ToString()));

                                if (reader["Billed1"].ToString() != "")
                                {
                                    Image1.Visible = true;
                                    Image1.ImageUrl = reader["BilledMappe"] + (string) reader["Billed1"] + "_thumb.jpg";
                                }

                                if (reader["Billed2"].ToString() != "")
                                {
                                    Image2.Visible = true;
                                    Image2.ImageUrl = reader["BilledMappe"] + (string) reader["Billed2"] + "_thumb.jpg";
                                }

                                if (reader["Billed3"].ToString() != "")
                                {
                                    Image3.Visible = true;
                                    Image3.ImageUrl = reader["BilledMappe"] + (string) reader["Billed3"] + "_thumb.jpg";
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
                    Label_Output.Text = "Fangsten kunne ikke hentes, kontakt webmasteren.";
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
            string billedMappe = "/Billeder/Fangster/" + TextBox_Aar.Text + "/";
            string filnavn1 = String.Empty;
            string filnavn2 = String.Empty;
            string filnavn3 = String.Empty;

            try
            {
                _connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                _connection.Open();

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

                string laengde = "Laengde = NULL, ";
                if (TextBox_Laengde.Text != String.Empty)
                {
                    laengde = "Laengde = '" + TextBox_Laengde.Text.Replace(",", ".") + "', ";
                }
                _commandText = "UPDATE fangster SET " +
                  "Forfatter = '" + User.Identity.Name + "', " +
                  "Dato = '" + TextBox_Aar.Text + "-" + DropDownList_Maaned.SelectedItem.Value + "-" + DropDownList_Dag.SelectedItem.Value + "', " +
                  "Art = '" + DropDownList_Art.SelectedItem.Value + "', " +
                  "Vaegt = '" + TextBox_Vaegt.Text.Replace(",", ".") + "', " +
                  laengde +
                  "Fanger = '" + DropDownList_Fanger.SelectedItem.Value + "', " +
                  "Fiskevand = '" + TextBox_Fiskevand.Text + "', " +
                  "Placering = '" + TextBox_Placering.Text + "', " +
                  "Land = '" + Textbox_Land.Text + "', " +
                  "Redskab = '" + TextBox_Redskab.Text + "', " +
                  "Vejr = '" + TextBox_Vejr.Text + "', " +
                  "Beskrivelse = '" + TextBox_Beskrivelse.Text + "', " +
                  "Link = '" + InternalHelper.CheckLinkAddress(TextBox_Link.Text) + "', " +
                  "BilledMappe = '" + billedMappe + "', ";

                if (filnavn1 != string.Empty)
                {
                    _commandText += "Billed1 = '" + filnavn1 + "', ";
                }

                if (filnavn2 != string.Empty)
                {
                    _commandText += "Billed2 = '" + filnavn2 + "', ";
                }

                if (filnavn3 != string.Empty)
                {
                    _commandText += "Billed3 = '" + filnavn3 + "', ";
                }

                _commandText += "Offentlig = '" + Convert.ToByte(CheckBox_Offentlig.Checked) + "', ";
                _commandText += "YearFish = '" + Convert.ToByte(CheckBox_YearFish.Checked) + "'" +
                  " WHERE FangstId = '" + _fangstId + "'";

                _command = new MySqlCommand(_commandText, _connection);
                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Label_Output.ForeColor = Color.Red;
                Label_Output.Text = ex.Message;
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }

                Response.Redirect(@"/Medlemmer/Default.aspx");
            }

        }
    }
}
