using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using Control;
using Model;

namespace Medlemmer
{
    public partial class MedlemmerIndtastBlogspot : Page
    {
        string _myConnection;

        protected void Page_Load(object sender, EventArgs e)
        {
            _myConnection = Application["MySQLConnString"].ToString();
            if (IsPostBack) { return; }

            try
            {
                OnPageLoad();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            try
            {
                OnDelete();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnInsertClick(object sender, EventArgs e)
        {
            try
            {
                OnInsertUpdate();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnUpdateClick(object sender, EventArgs e)
        {
            try
            {
                OnInsertUpdate();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void DrpSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OnSelectedIndexChanged();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        #region Private Metods

        private void OnPageLoad()
        {
            ClearControls();
            ValidateControls();
            InitiateControls();
        }

        private void OnDelete()
        {
            var ctrl = new BlogspotController();
            ctrl.Delete(_myConnection, Convert.ToInt32(txtId.Text));

            ClearControls();
            InitiateControls();

            lblMessage.Text = @"Data er slettet!";
        }

        private void OnInsertUpdate()
        {
            OnPreValidation();

            lblMessage.Text = string.Empty;
            if (IsValid)
            {
                try
                {
                    var id = Convert.ToInt32(txtId.Text);
                    var user = User.Identity.Name;
                    var date = DateTimeHelper.StringToDateInDanishFormat(txtDate.Text);
                    var username = txtUsername.Text;
                    var password = InternalHelper.EncryptString(txtPassword.Text);
                    var url = txtUrl.Text;
                    var blogspot = new BlogspotObject(id, user, date, username, password, url);

                    var ctrl = new BlogspotController();
                    if (blogspot.Id == 0)
                    {
                        txtId.Text = ctrl.Insert(_myConnection, blogspot).ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        ctrl.Update(_myConnection, blogspot);
                    }

                    ClearControls();
                    InitiateControls();

                    lblMessage.Text = @"Data er gemt!";
                }
                catch (Exception ex)
                {
                    var ctrl = new SysExceptionController();
                    ctrl.OnException(ex, 0);
                }
            }
        }

        private void OnSelectedIndexChanged()
        {
            ClearControls();

            if (drpSelect.SelectedIndex < 1)
            {
                InitiateControls();
                return;
            }

            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;

            var ctrl = new BlogspotController();
            BlogspotObject blogspot = ctrl.Select(_myConnection, Convert.ToInt32(drpSelect.SelectedValue));
            if (blogspot != null)
            {
                txtId.Text = blogspot.Id.ToString(CultureInfo.InvariantCulture);
                txtDate.Text = DateTimeHelper.DateToStringInGuiFormat(blogspot.Date);
                txtUser.Text = blogspot.User;
                txtUsername.Text = blogspot.Username;
                txtPassword.Text = InternalHelper.DecryptString(blogspot.Password);
                txtUrl.Text = blogspot.Url;

                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
        }

        private void InitiateControls()
        {
            var ctrl = new BlogspotController();
            ctrl.Load(_myConnection, User, drpSelect);

            var item = new ListItem { Text = @"-- Vælg --", Value = @"0" };
            drpSelect.Items.Insert(0, item);
            drpSelect.SelectedIndex = 0;

            txtId.Text = @"0";
            txtDate.Text = DateTimeHelper.DateToStringInGuiFormat(DateTime.Now);
            txtUser.Text = User.Identity.Name;

            btnInsert.Enabled = true;
            drpSelect.Focus();
        }

        private void ClearControls()
        {
            txtId.Text = string.Empty;
            txtDate.Text = string.Empty;
            txtUser.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUrl.Text = @"eksempel.blogspot.dk";

            btnDelete.Enabled = false;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;

            lblMessage.Text = string.Empty;
        }

        private void ValidateControls()
        {
            rfvUsername.ControlToValidate = txtUsername.ID;
            rfvUsername.EnableClientScript = true;
            rfvUsername.ErrorMessage = @"Brugernavn mangler!";
            rfvUsername.Text = @"*";
            rfvUsername.SetFocusOnError = true;

            rfvPassword.ControlToValidate = txtPassword.ID;
            rfvPassword.EnableClientScript = true;
            rfvPassword.ErrorMessage = @"Kode mangler - eller er forkert!";
            rfvPassword.Text = @"*";
            rfvPassword.SetFocusOnError = true;

            rfvUrl.ControlToValidate = txtUrl.ID;
            rfvUrl.EnableClientScript = true;
            rfvUrl.ErrorMessage = @"Link/URL mangler!";
            rfvUrl.Text = @"*";
            rfvUrl.SetFocusOnError = true;
            revUrl.ValidationExpression = @"^([a-zA-Z0-9])+(.(blogspot.com|blogspot.dk)?)$";

            revUrl.ControlToValidate = txtUrl.ID;
            revUrl.EnableClientScript = true;
            revUrl.ErrorMessage = @"Link/URL er forkert! Det skal passe til eksemplet [<i>eksempel.blogspot.dk</i>]. Forstået?";
            revUrl.Text = @"*";
            revUrl.SetFocusOnError = true;

            vsMessage.DisplayMode = ValidationSummaryDisplayMode.BulletList;
            vsMessage.EnableClientScript = true;
            vsMessage.HeaderText = @"Oprettelse af en Blog er afbrudt! Der er en eller flere fejl.";
            vsMessage.ShowSummary = true;
        }

        private void OnPreValidation()
        {
            var id = Convert.ToInt32(txtId.Text);
            var user = User.Identity.Name;
            var date = DateTimeHelper.StringToDateInDanishFormat(txtDate.Text);
            var username = txtUsername.Text;
            var password = InternalHelper.EncryptString(txtPassword.Text);
            var url = txtUrl.Text;
            var blogspot = new BlogspotObject(id, user, date, username, password, url);

            var ctrl = new BlogspotController();
            if (!ctrl.Validate(blogspot))
            {
                txtPassword.Text = string.Empty;
                Page.Validate();                
            }
        }
        #endregion
    }
}