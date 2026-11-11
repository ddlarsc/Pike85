using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;
using Control;

namespace Administration
{
    public partial class Medlem:SkabelonPage
    {
        private string _myConnection;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender,EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _myConnection = Application["MySQLConnString"].ToString();
            lblMessage.Text = String.Empty;

            if(!Page.IsPostBack)
            {
                try
                {
                    OnPageLoad();
                    InternalHelper.SetFocus(drpUserList);
                    btnDeleteUser.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this menber?');");
                }
                catch(Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
        }

        protected void UserIdSelectedIndexChanged(object sender,EventArgs e)
        {
            try
            {
                OnSelectUser();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnUpdateUserClick(object sender,EventArgs e)
        {
            try
            {
                OnCreateUpdateUser(BrugerEnum.Update);
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnCreateUserClick(object sender,EventArgs e)
        {
            try
            {
                OnCreateUpdateUser(BrugerEnum.Create);
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnDeleteUserClick(object sender,EventArgs e)
        {
            try
            {
                OnDeleteUser();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        #region Private Methods

        private void OnPageLoad()
        {
            txtBrugerId.Enabled = true;
            btnDeleteUser.Enabled = false;
            btnInsertUser.Enabled = true;
            btnUpdateUser.Enabled = false;

            GetUserList();
            InitializeValidators();

            //Setting up the Calendar Icons
            pickDate1.Attributes.Add("onclick","cal1.select(" + IdentifyCtrl(txtMembership) + ",'" + IdentifyCtrl(anchorCal) + "','dd-MM-yyyy', getDate(" + IdentifyCtrl(txtMembership) + ")); return false;");
            pickDate1.Attributes.Add("src",Resources.Pike85.IconCalendarDay);
            pickDate2.Attributes.Add("onclick","cal1.select(" + IdentifyCtrl(txtBirthDay) + ",'" + IdentifyCtrl(anchorCal) + "','dd-MM-yyyy', getDate(" + IdentifyCtrl(txtBirthDay) + ")); return false;");
            pickDate2.Attributes.Add("src",Resources.Pike85.IconCalendarDay);
        }

        private void OnSelectUser()
        {
            txtBrugerId.Enabled = false;
            btnDeleteUser.Enabled = false;
            btnInsertUser.Enabled = false;
            btnUpdateUser.Enabled = false;

            FillControl(drpUserList.SelectedValue);

            if(drpUserList.SelectedIndex < 1)
            {
                txtBrugerId.Enabled = true;
                btnInsertUser.Enabled = true;
            }
            else if(drpUserList.SelectedIndex > 0)
            {
                btnDeleteUser.Enabled = true;
                btnUpdateUser.Enabled = true;
            }
        }

        private void OnCreateUpdateUser(BrugerEnum type)
        {
            if(IsValid)
            {
                string filename = Page.Server.MapPath(string.Format("~/Billeder/Medlemmer/{0}.gif", txtBrugerId.Text));
                if(FileUpload.PostedFile != null && FileUpload.PostedFile.ContentLength != 0)
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload.PostedFile.InputStream);
                    img.Save(filename, System.Drawing.Imaging.ImageFormat.Gif);
                }

                var birthday = DateTimeHelper.StringToDateInDanishFormat(txtBirthDay.Text);
                var city = txtCity.Text;
                const bool isDead = false;
                var description = txtDescription.Text;
                var email1 = txtEmail1.Text;
                var email2 = txtEmail2.Text;
                var enrolled = DateTimeHelper.StringToDateInDanishFormat(txtMembership.Text);
                var image = txtBrugerId.Text;
                var name = txtNavn.Text;
                var phoneMobil = txtMobilPhone.Text;
                var phonePrivate = txtHjemTlf.Text;
                var phoneWork = txtArbTlf.Text;
                var isPublic = ckbOffentlig.Checked;
                var shortName = txtKortNavn.Text;
                var street = txtAdresse.Text;
                var userCode = FormsAuthentication.HashPasswordForStoringInConfigFile("Gedde", "SHA1");
                var userLevel = drpUserLevel.SelectedValue;
                var userId = txtBrugerId.Text;
                var zipCode = txtZipCode.Text;

                var bruger = new BrugerObject(userId, userCode, name, shortName, birthday, description, email1, image, userLevel, isPublic, street, city, zipCode, email2, phonePrivate, phoneWork, phoneMobil, enrolled, isDead);
                var ctrl = new BrugerController();

                switch (type)
                {
                    case BrugerEnum.Create:

                        ctrl.Create(_myConnection, bruger);
                        GetUserList();
                        ClearForm();
                        lblMessage.Text = @"Bruger er oprettet!";
                        break;

                    case BrugerEnum.Update:

                        ctrl.Update(_myConnection, bruger);
                        lblMessage.Text = @"Brugeren er opdateret!";
                        break;
                }
            }
        }

        private void OnDeleteUser()
        {
            var ctrl = new BrugerController();
            ctrl.Delete(_myConnection, drpUserList.SelectedValue);
            lblMessage.Text = @"Bruger er slettet!";

            GetUserList();
            ClearForm();
        }

        private void InitializeValidators()
        {
            //Textfields
            rfvKortNavn.ControlToValidate = txtKortNavn.ID;
            rfvKortNavn.ErrorMessage = @"Kort navn mangler!";
            rfvKortNavn.SetFocusOnError = true;
            rfvKortNavn.Text = @"*";

            rfvAddress.ControlToValidate = txtAdresse.ID;
            rfvAddress.ErrorMessage = @"Adressen mangler!";
            rfvAddress.SetFocusOnError = true;
            rfvAddress.Text = @"*";

            rfvBirthday.ControlToValidate = txtBirthDay.ID;
            rfvBirthday.ErrorMessage = @"Fødselsdagdato mangler!";
            rfvBirthday.SetFocusOnError = true;
            rfvBirthday.Text = @"*";

            rfvCity.ControlToValidate = txtCity.ID;
            rfvCity.ErrorMessage = @"By mangler!";
            rfvCity.SetFocusOnError = true;
            rfvCity.Text = @"*";

            rfvDescription.ControlToValidate = txtDescription.ID;
            rfvDescription.ErrorMessage = @"Beskrivelse mangler!";
            rfvDescription.Text = @"*";
            rfvDescription.SetFocusOnError = true;

            rfvEmail1.ControlToValidate = txtEmail1.ID;
            rfvEmail1.ErrorMessage = @"Email mangler!";
            rfvEmail1.Text = @"*";
            rfvEmail1.SetFocusOnError = true;

            revEmail1.ControlToValidate = txtEmail1.ID;
            revEmail1.ErrorMessage = @"Email er ikke korrekt format!";
            revEmail1.SetFocusOnError = true;
            revEmail1.ValidationExpression = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            revEmail1.Text = @"*";

            revEmail2.ControlToValidate = txtEmail2.ID;
            revEmail2.ErrorMessage = @"Email er ikke korrekt format!";
            revEmail2.SetFocusOnError = true;
            revEmail2.ValidationExpression = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            revEmail2.Text = @"*";

            rfvMembership.ControlToValidate = txtMembership.ID;
            rfvMembership.ErrorMessage = @"Indmeldelsesdato mangler!";
            rfvMembership.SetFocusOnError = true;
            rfvMembership.Text = @"*";

            rfvBrugerId.ControlToValidate = txtBrugerId.ID;
            rfvBrugerId.ErrorMessage = @"Kort navn mangler!";
            rfvBrugerId.SetFocusOnError = true;
            rfvBrugerId.Text = @"*";

            rfvMobilTlf.ControlToValidate = txtMobilPhone.ID;
            rfvMobilTlf.ErrorMessage = @"Mobiltelefon mangler";
            rfvMobilTlf.SetFocusOnError = true;
            rfvMobilTlf.Text = @"*";

            rfvNavn.ControlToValidate = txtNavn.ID;
            rfvNavn.ErrorMessage = @"Navn mangler!";
            rfvNavn.SetFocusOnError = true;
            rfvNavn.Text = @"*";

            rfvPostnr.ControlToValidate = txtZipCode.ID;
            rfvPostnr.ErrorMessage = @"Postnr. mangler!";
            rfvPostnr.SetFocusOnError = true;
            rfvPostnr.Text = @"*";

            revZipCode.EnableClientScript = true;
            revZipCode.ControlToValidate = txtZipCode.ID;
            revZipCode.ErrorMessage = @"Postnummer skal være 4 cifre langt!";
            revZipCode.SetFocusOnError = true;
            revZipCode.ValidationExpression = @"^[0-9]{4}";
            revZipCode.Text = @"!";

            //Summary
            vldSummery.DisplayMode = ValidationSummaryDisplayMode.BulletList;
            vldSummery.HeaderText = @"Oprettelse af et arrangement er afbrudt! Der er en eller flere fejl.";
            vldSummery.ShowSummary = true;
        }

        private void GetUserList()
        {
            var ctrl = new BrugerController();
            ctrl.Get(_myConnection, drpUserList);

            //Number 0!
            var item = new ListItem { Text = @"-- Vælg --",Value = @"0" };
            drpUserList.Items.Insert(0,item);
            drpUserList.SelectedIndex = 0;
        }

        private static string IdentifyCtrl(System.Web.UI.Control ctrl)
        {
            return ctrl.UniqueID.Replace(":","_");
        }

        private void FillControl(string id)
        {
            ClearForm();

            if(id != "0")
            {
                var ctrl = new BrugerController();
                BrugerObject bruger = ctrl.Get(_myConnection,id);
                if(bruger != null)
                {
                    txtBrugerId.Text = bruger.UserId;
                    txtNavn.Text = bruger.Name;
                    txtKortNavn.Text = bruger.ShortName;
                    txtMembership.Text = bruger.Enrolled.ToShortDateString();
                    txtBirthDay.Text = bruger.Birthday.ToShortDateString();
                    txtDescription.Text = bruger.Description;
                    txtEmail1.Text = bruger.Email1;
                    txtEmail2.Text = bruger.Email2;
                    txtAdresse.Text = bruger.Street;
                    txtCity.Text = bruger.City;
                    txtZipCode.Text = bruger.ZipCode;
                    txtHjemTlf.Text = bruger.PhonePrivate;
                    txtArbTlf.Text = bruger.PhoneWork;
                    txtMobilPhone.Text = bruger.PhoneMobil;
                    imgMedlem.ImageUrl = bruger.Image;
                    imgMedlem.Visible = !string.IsNullOrEmpty(imgMedlem.ImageUrl);
                    drpUserLevel.SelectedValue = bruger.UserLevel;
                    ckbOffentlig.Checked = bruger.Public;
                }
            }
        }

        private void ClearForm()
        {
            txtBrugerId.Text = string.Empty;
            txtNavn.Text = string.Empty;
            txtKortNavn.Text = string.Empty;
            txtMembership.Text = DateTimeHelper.DateToStringInGuiFormat(DateTime.Today);
            txtBirthDay.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEmail1.Text = string.Empty;
            txtEmail2.Text = string.Empty;
            txtAdresse.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            txtHjemTlf.Text = string.Empty;
            txtMobilPhone.Text = string.Empty;
            txtArbTlf.Text = string.Empty;
            imgMedlem.Visible = false;
            drpUserLevel.SelectedIndex = -1;
            ckbOffentlig.Checked = true;

            txtBrugerId.Enabled = false;
            btnDeleteUser.Enabled = false;
            btnInsertUser.Enabled = false;
            btnUpdateUser.Enabled = false;
        }

        #endregion
    }
}
