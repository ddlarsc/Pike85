using System;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using App_Code.Model;
using Control;

namespace Administration
{
    public partial class Admin : SkabelonPage
    {
        private string _connection;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _connection = Application["MySQLConnString"].ToString();
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

        protected void BtnCreateClick(object sender, EventArgs e)
        {
            try
            {
                OnInsert();
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
                OnUpdate();
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

        protected void DrpGetReferatSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OnDropdownlistSelect();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        #region Private Methods

        private void OnPageLoad()
        {
            InitiateValidators();
            LoadDrp();
            InternalHelper.SetFocus(drpGetReferat);
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Generalforsamling?');");
        }

        private void EnableButtons(bool isEnable)
        {
            btnCreate.Enabled = isEnable;
            btnDelete.Enabled = isEnable;
            btnUpdate.Enabled = isEnable;
        }

        private void InitiateValidators()
        {
            rfvBody.ControlToValidate = txtBody.ID;
            rfvBody.EnableClientScript = true;
            rfvBody.ErrorMessage = @"Beskrivelse mangler!";
            rfvBody.SetFocusOnError = true;
            rfvBody.Text = @"*";

            rfvFooter.ControlToValidate = txtFooter.ID;
            rfvFooter.EnableClientScript = true;
            rfvFooter.ErrorMessage = @"Fodnote mangler!";
            rfvFooter.SetFocusOnError = true;
            rfvFooter.Text = @"*";

            rfvHeader.ControlToValidate = txtHeader.ID;
            rfvHeader.EnableClientScript = true;
            rfvHeader.ErrorMessage = @"Overskrift mangler!";
            rfvHeader.SetFocusOnError = true;
            rfvHeader.Text = @"*";

            rfvLinkText.ControlToValidate = txtLinkText.ID;
            rfvLinkText.EnableClientScript = true;
            rfvLinkText.ErrorMessage = @"Link-tekst mangler!";
            rfvLinkText.SetFocusOnError = true;
            rfvLinkText.Text = @"*";

            vsGeneralforsamling.DisplayMode = ValidationSummaryDisplayMode.BulletList;
            vsGeneralforsamling.EnableClientScript = true;
            vsGeneralforsamling.HeaderText = @"Oprettelse af et Referat er afbrudt! Der er en eller flere fejl.";
            vsGeneralforsamling.ShowSummary = true;
        }

        private void LoadDrp()
        {
            ClearControls();
            EnableButtons(false);

            var ctrl = new AdminGeneralforsamlingController();
            ctrl.SelectAll(_connection, drpGetReferat, "HEADER", "PK");

            drpGetReferat.Items.Insert(0, "-- Vælg --");
            drpGetReferat.SelectedIndex = -1;
            drpGetReferat.Enabled = true;
            btnCreate.Enabled = true;
        }

        private void ClearControls()
        {
            txtBody.Text = string.Empty;
            txtFooter.Text = string.Empty;
            txtHeader.Text = string.Empty;
            txtLinkText.Text = string.Empty;
            fuLink.Dispose();
            lblMessage.Text = string.Empty;
        }

        private void OnInsert()
        {
            if (!fuLink.HasFile)
            {
                lblMessage.Text = @"Link mangler!";
                fuLink.Focus();
                return;
            }

            if (IsValid)
            {
                //save the file to the server 
                fuLink.PostedFile.SaveAs(Server.MapPath(".\\Documents\\") + fuLink.FileName);

                var generalforsamlingObject = new GeneralforsamlingObject
                {
                    Body = txtBody.Text,
                    Footer = txtFooter.Text,
                    Header = txtHeader.Text,
                    Link = fuLink.FileName,
                    LinkText = txtLinkText.Text,
                    Pk = 0,
                    Signature = User.Identity.Name,
                    Timestamp = DateTime.Now
                };

                var ctrl = new AdminGeneralforsamlingController();
                ctrl.Insert(_connection, generalforsamlingObject);

                LoadDrp();
                lblMessage.Text = @"Data gemt succesfuldt!";
            }
        }

        private void OnUpdate()
        {
            if (IsValid)
            {
                if (fuLink.HasFile)
                {
                    var fileInfo = new FileInfo(Server.MapPath(".\\Documents\\") + lblLink.Text);
                    fileInfo.Delete();

                    fuLink.PostedFile.SaveAs(Server.MapPath(".\\Documents\\") + fuLink.FileName);
                }

                var generalforsamlingObject = new GeneralforsamlingObject
                {
                    Body = txtBody.Text,
                    Footer = txtFooter.Text,
                    Header = txtHeader.Text,
                    Link = fuLink.FileName,
                    LinkText = txtLinkText.Text,
                    Pk = int.Parse(lblPrimaryKey.Text),
                    Signature = Context.User.Identity.Name,
                    Timestamp = DateTime.Now
                };

                var ctrl = new AdminGeneralforsamlingController();
                ctrl.Update(_connection, generalforsamlingObject);

                LoadDrp();
                lblMessage.Text = @"Data opdateret succesfuldt!";
            }
        }

        private void OnDelete()
        {
            var ctrl = new AdminGeneralforsamlingController();
            GeneralforsamlingObject generalforsamlingObject = ctrl.Select(_connection, int.Parse(drpGetReferat.SelectedValue));

            var fileInfo = new FileInfo(Server.MapPath(".\\Documents\\") + generalforsamlingObject.Link);
            fileInfo.Delete();

            ctrl.Delete(_connection, int.Parse(drpGetReferat.SelectedValue));

            EnableButtons(false);
            LoadDrp();
            lblMessage.Text = @"Sletning af data gennemført!";
        }

        private void OnDropdownlistSelect()
        {
            EnableButtons(false);
            if (drpGetReferat.SelectedIndex < 1)
            {
                ClearControls();
                btnCreate.Enabled = true;
            }
            else
            {
                FillControls();
            }
        }

        private void FillControls()
        {
            var ctrl = new AdminGeneralforsamlingController();
            GeneralforsamlingObject generalforsamling = ctrl.Select(_connection, int.Parse(drpGetReferat.SelectedValue));
            if (generalforsamling != null)
            {
                lblMessage.Text = string.Empty;
                lblPrimaryKey.Text = generalforsamling.Pk.ToString(CultureInfo.InvariantCulture);
                lblLink.Text = generalforsamling.Link;
                txtBody.Text = generalforsamling.Body;
                txtFooter.Text = generalforsamling.Footer;
                txtHeader.Text = generalforsamling.Header;
                txtLinkText.Text = generalforsamling.LinkText;

                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
                fuLink.Enabled = true;
            }
        }

        #endregion
    }
}
