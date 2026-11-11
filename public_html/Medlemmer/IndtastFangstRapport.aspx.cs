using System;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;
using Control;

namespace Medlemmer
{
    public partial class IndtastFangstRapport : Page
    {
        readonly NumberFormatInfo _nfi = new NumberFormatInfo
        {
            NegativeSign = "-",
            NumberDecimalDigits = 2,
            NumberDecimalSeparator = ",",
            NumberGroupSeparator = string.Empty,
        };

        string _myConnection;
        protected void Page_Load(object sender, EventArgs e)
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if(IsPostBack) {return;}

            try
            {
                OnPageLoad();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void DrpOpslagSelectedIndexChanged(object sender,EventArgs e)
        {
            try
            {
                OnSelectedIndexChanged();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnInsertClick(object sender,EventArgs e)
        {
            try
            {
                OnInsertUpdate();
                FillHeader();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnUpdateClick(object sender,EventArgs e)
        {
            try
            {
                OnInsertUpdate();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnDeleteClick(object sender,EventArgs e)
        {
            try
            {
                OnDelete();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        #region Private Methods

        private void OnPageLoad()
        {
            //Setting up the Calendar Icons
            pickDate1.Attributes.Add("onclick","cal1.select(" + IdentifyCtrl(txtDato) + ",'" + IdentifyCtrl(anchorCal) + "','dd-MM-yyyy', getDate(" + IdentifyCtrl(txtDato) + ")); return false;");
            pickDate1.Attributes.Add("src",Resources.Pike85.IconCalendarDay);

            var ctrl = new ArterController();
            ctrl.Get(_myConnection, drpArt);
            
            ClearControls();
            InitControls();

            bool isAdministrator = User.IsInRole("Administrator");
            lblYearFish.Text = @"Er dette årets fiske:";
            lblYearFish.Visible = isAdministrator;
            chkYearFish.Visible = isAdministrator;

            InitializeValidators();
            btnInsert.Enabled = true;
            btnDelete.Attributes.Add("onclick","return confirm('Er du sikker på, at du vil slette denne fangst?');");
        }

        private void OnSelectedIndexChanged()
        {
            ClearControls();

            if(drpFangster.SelectedIndex < 1)
            {
                InitControls();
                return;
            }

            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;

            var ctrl = new FangsterController();
            FangsterObject fangst = ctrl.Get(_myConnection, Convert.ToInt32(drpFangster.SelectedValue));
            if(fangst != null)
            {
                drpArt.SelectedItem.Text = fangst.Art;
                drpFanger.SelectedItem.Text = fangst.Fanger;

                txtBeskrivelse.Text = fangst.Beskrivelse;
                txtDato.Text = DateTimeHelper.DateToStringInGuiFormat(fangst.Dato);
                txtFanger.Text = fangst.Forfatter;
                txtFiskevand.Text = fangst.Fiskevand;
                txtId.Text = fangst.FangstId.ToString(_nfi);
                txtLand.Text = fangst.Land;
                txtLink.Text = fangst.Link;
                txtLængde.Text = fangst.Laengde.ToString(_nfi);
                txtPlacering.Text = fangst.Placering;
                txtRedskab.Text = fangst.Redskab;
                txtVejr.Text = fangst.Vejr;
                txtVægt.Text = fangst.Vaegt.ToString(_nfi);

                LoadImageIntoControl(imgBilled1,fangst.BilledMappe,fangst.Billed1);
                LoadImageIntoControl(imgBilled2,fangst.BilledMappe,fangst.Billed2);
                LoadImageIntoControl(imgBilled3,fangst.BilledMappe,fangst.Billed3);

                chkOffentlig.Checked = fangst.Offentlig;
                chkYearFish.Checked = fangst.YearFish;

                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
        }

        private void OnInsertUpdate()
        {
            lblMessage.Text = string.Empty;
            if(IsValid)
            {
                try
                {
                    var billedMappe = string.Format("/Billeder/Fangster/{0}/", DateTimeHelper.StringToDateInDanishFormat(txtDato.Text).Year);
                    var filnavn1 = SaveImage(fuBilled1, rbnBilled1Staaende, imgBilled1, billedMappe);
                    var filnavn2 = SaveImage(fuBilled2, rbnBilled2Staaende, imgBilled2, billedMappe);
                    var filnavn3 = SaveImage(fuBilled3, rbnBilled3Staaende, imgBilled3, billedMappe);

                    var art = drpArt.SelectedItem.Text;
                    var beskrivelse = txtBeskrivelse.Text;
                    DateTime dato; DateTime.TryParse(txtDato.Text, out dato);
                    var fanger = drpFanger.SelectedItem.Text;
                    int fangstId;
                    int.TryParse(txtId.Text, out fangstId);
                    var fiskevand = txtFiskevand.Text;
                    var forfatter = User.Identity.Name;
                    double laengde; Double.TryParse(txtLængde.Text, out laengde);
                    var land = txtLand.Text;
                    var link = txtLink.Text;
                    var offentlig = chkOffentlig.Checked;
                    var placering = txtPlacering.Text;
                    var redskab = txtRedskab.Text;
                    double vaegt; Double.TryParse(txtVægt.Text, out vaegt);
                    var vejr = txtVejr.Text;
                    var video1 = string.Empty;
                    var video2 = string.Empty;
                    var video3 = string.Empty;
                    var yearFish = chkYearFish.Checked;
                    var fangst = new FangsterObject(fangstId, forfatter, dato, art, vaegt, laengde, fanger, fiskevand, placering, land, redskab, vejr, beskrivelse, link, billedMappe, filnavn1, filnavn2, filnavn3, video1, video2, video3, offentlig, yearFish);

                    var fangsterController = new FangsterController();
                    if(fangst.FangstId == 0)
                    {
                        txtId.Text = fangsterController.Insert(_myConnection,fangst).ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        fangsterController.Update(_myConnection,fangst);
                    }

                    btnInsert.Enabled = false;
                    lblMessage.Text = @"Data er gemt!";
                }
                catch(Exception ex)
                {
                    var ctrl = new SysExceptionController();
                    ctrl.OnException(ex, 0);
                }
            }
        }

        private string SaveImage(FileUpload fuCtrl, RadioButton rbCtrl, Image imgCtrl, string folder)
        {
            //Exsisting file name
            var fileName = imgCtrl.ImageUrl.Replace("~",string.Empty).Replace(folder,string.Empty).Replace(@"_thumb",string.Empty).Replace(@".jpg",string.Empty); //Todo - må kunne gøre pænere?
            if(fuCtrl.HasFile)
            {
                DeleteOldImage(imgCtrl);

                //New file name.
                fileName = InternalHelper.OpretBilledFiler(Page.Server.MapPath(string.Format("~{0}", folder)), fuCtrl, rbCtrl.Checked);
                LoadImageIntoControl(imgCtrl, folder, fileName);
            }
            return fileName;
        }

        private void DeleteOldImage(Image imgCtrl)
        {
            if(!string.IsNullOrEmpty(imgCtrl.ImageUrl))
            {
                File.Delete(Server.MapPath(imgCtrl.ImageUrl)); //Delete _thumb picture.
                File.Delete(Server.MapPath(imgCtrl.ImageUrl.Replace(@"_thumb", string.Empty))); //Delete ordinary picture.
            }
        }

        private void OnDelete()
        {
            DeleteOldImage(imgBilled1);
            DeleteOldImage(imgBilled2);
            DeleteOldImage(imgBilled3);

            var ctrl = new FangsterController();
            ctrl.Delete(_myConnection, Convert.ToInt32(txtId.Text));

            ClearControls();
            InitControls();

            lblMessage.Text = @"Data er slettet!";
        }

        private static string IdentifyCtrl(System.Web.UI.Control ctrl)
        {
            return ctrl.UniqueID.Replace(":","_");
        }

        private void InitializeValidators()
        {
            //Textfields
            rfvDato.ControlToValidate = txtDato.ID;
            rfvDato.EnableClientScript = true;
            rfvDato.ErrorMessage = @"Dato mangler!";
            rfvDato.Text = @"*";
            rfvDato.SetFocusOnError = true;

            revDato.ControlToValidate = txtDato.ID;
            revDato.EnableClientScript = true;
            revDato.ErrorMessage = @"Dato skal være DD-MM-ÅÅÅÅ format!";
            revDato.SetFocusOnError = true;
            revDato.Text = @"!";
            revDato.ValidationExpression = "\\d{2}-\\d{2}-\\d{4}";

            rfvFiskevand.ControlToValidate = txtFiskevand.ID;
            rfvFiskevand.EnableClientScript = true;
            rfvFiskevand.ErrorMessage = @"Fiskevand mangler!";
            rfvFiskevand.Text = @"*";
            rfvFiskevand.SetFocusOnError = true;

            rfvLand.ControlToValidate = txtLand.ID;
            rfvLand.EnableClientScript = true;
            rfvLand.ErrorMessage = @"Land mangler!";
            rfvLand.Text = @"*";
            rfvLand.SetFocusOnError = true;

            rfvVægt.ControlToValidate = txtVægt.ID;
            rfvVægt.EnableClientScript = true;
            rfvVægt.ErrorMessage = @"Vægten mangler!";
            rfvVægt.Text = @"*";
            rfvVægt.SetFocusOnError = true;

            //Summary
            vsMessage.DisplayMode = ValidationSummaryDisplayMode.BulletList;
            vsMessage.EnableClientScript = true;
            vsMessage.HeaderText = @"Oprettelse af et fangstrapporten er afbrudt! Der er en eller flere fejl.";
            vsMessage.ShowSummary = true;
        }

        private void InitControls()
        {
            FillHeader();

            var ctrl = new BrugerController();
            ctrl.Get(_myConnection, drpFanger);
            
            var item = new ListItem { Text = @"-- Vælg --", Value = @"0" };
            drpFanger.Items.Insert(0, item);
            drpFanger.SelectedIndex = 0;
            
            txtDato.Text = DateTimeHelper.DateToStringInGuiFormat(DateTime.Now);
            txtFanger.Text = User.Identity.Name;
            txtId.Text = @"0";
            txtLand.Text = @"Danmark";

            btnInsert.Enabled = true;
            drpFangster.Focus();
        }

        private void FillHeader()
        {
            var ctrl = new FangsterController();
            ctrl.Get(_myConnection, User, drpFangster);

            var item = new ListItem { Text = @"-- Vælg --", Value = @"0" };
            drpFangster.Items.Insert(0, item);
            drpFangster.SelectedIndex = 0;
        }

        private void ClearControls()
        {
            drpArt.SelectedIndex = 0;
            drpFanger.SelectedIndex = 0;

            txtBeskrivelse.Text = string.Empty;
            txtDato.Text = string.Empty;
            txtFanger.Text = string.Empty;
            txtFiskevand.Text = string.Empty;
            txtId.Text = string.Empty;
            txtLand.Text = string.Empty;
            txtLink.Text = string.Empty;
            txtLængde.Text = string.Empty;
            txtPlacering.Text = string.Empty;
            txtRedskab.Text = string.Empty;
            txtVejr.Text = string.Empty;
            txtVægt.Text = string.Empty;

            imgBilled1.ImageUrl = string.Empty;
            imgBilled1.Visible = false;
            imgBilled2.ImageUrl = string.Empty;
            imgBilled2.Visible = false;
            imgBilled3.ImageUrl = string.Empty;
            imgBilled3.Visible = false;

            chkOffentlig.Checked = false;
            chkYearFish.Checked = false;

            btnDelete.Enabled = false;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;

            lblMessage.Text = string.Empty;
        }

        private void LoadImageIntoControl(Image image, string folder, string picture)
        {
            //Max heigt 52px & Max width 150px
            if(string.IsNullOrEmpty(picture))
            {
                image.Visible = false;
            }
            else
            {
                try
                {
                    string imageUrl = string.Format("{0}{1}_thumb.jpg",folder,picture);
                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(imageUrl));
                    double reduction = 52 / (double)img.Height;
                    image.AlternateText = @"Image missing!";
                    image.ImageAlign = ImageAlign.Right;
                    image.ImageUrl = imageUrl;
                    image.Height = (Unit)(img.Height * reduction);
                    image.Visible = true;
                    image.Width = (Unit)(img.Width * reduction);
                }
                catch
                {
                    image.Visible = false;
                }
            }
        }

        #endregion
    }
}
