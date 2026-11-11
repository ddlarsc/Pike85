using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;
using Control;

namespace Medlemmer
{
    public partial class IndtastOpslag : Page
    {
        string _myConnection;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if (IsPostBack) { return; }

            try
            {
                OnpageLoad();
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

        protected void DrpOpslagSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OnIndexChange();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        #region Private Methods

        private void OnpageLoad()
        {
            //Setting up the Calendar Icons
            pickDate1.Attributes.Add("onclick", "cal1.select(" + IdentifyCtrl(txtDato) + ",'" + IdentifyCtrl(anchorCal) + "','dd-MM-yyyy', getDate(" + IdentifyCtrl(txtDato) + ")); return false;");
            pickDate1.Attributes.Add("src", Resources.Pike85.IconCalendarDay);

            InitializeValidate();
            ClearControls();
            FillControls();

            drpOpslag.Focus();
            btnDelete.Attributes.Add("onclick", "return confirm('Er du sikker på, at du vil slette dette opslag?');");
        }

        private void OnInsertUpdate()
        {
            lblMessage.Text = string.Empty;
            if (IsValid)
            {
                DateTime dato = DateTimeHelper.StringToDateInDanishFormat(txtDato.Text);
                string billedMappe = string.Empty;
                string filnavn = string.Empty;

                if (fuBilled.HasFile)
                {
                    billedMappe = "/Billeder/Opslag/" + dato.Year + "/";
                    filnavn = InternalHelper.OpretBilledFiler(Page.Server.MapPath(billedMappe), fuBilled, rbnBilledStaaende.Checked);
                }

                var opslag = new OpslagObject
                {
                    Beskrivelse = txtBeskrivelse.Text,
                    Billed = filnavn,
                    BilledMappe = billedMappe,
                    Dato = dato,
                    Forfatter = User.Identity.Name,
                    Link = txtLink.Text,
                    Offentlig = chkOffentlig.Checked,
                    Overskrift = txtOverskrift.Text,
                    Video1 = string.Empty,
                };

                var ctrl = new OpslagController();
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    ctrl.Insert(_myConnection, opslag);
                }
                else
                {
                    opslag.OpslagId = Convert.ToInt32(txtId.Text);
                    ctrl.Update(_myConnection, opslag);
                }

                ClearControls();
                FillControls();
                lblMessage.Text = @"Data er gemt!";
            }
        }

        private void OnDelete()
        {
            var ctrl = new OpslagController();
            ctrl.Delete(_myConnection, Convert.ToInt32(drpOpslag.SelectedValue));

            ClearControls();
            FillControls();
            lblMessage.Text = @"Data er slettet!";
        }

        private void OnIndexChange()
        {
            ClearControls();

            if (drpOpslag.SelectedIndex < 1)
            {
                btnInsert.Enabled = true;
                return;
            }

            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;

            var ctrl = new OpslagController();
            OpslagObject opslag = ctrl.Get(_myConnection, Convert.ToInt32(drpOpslag.SelectedValue));

            txtBeskrivelse.Text = opslag.Beskrivelse;
            txtDato.Text = DateTimeHelper.DateToStringInGuiFormat(opslag.Dato);
            txtId.Text = opslag.OpslagId.ToString(CultureInfo.InvariantCulture);
            txtForfatter.Text = opslag.Forfatter;
            txtLink.Text = opslag.Link;
            chkOffentlig.Checked = opslag.Offentlig;
            txtOverskrift.Text = opslag.Overskrift;

            txtDato.Focus();
        }

        private static string IdentifyCtrl(System.Web.UI.Control ctrl)
        {
            return ctrl.UniqueID.Replace(":", "_");
        }

        private void InitializeValidate()
        {
            rfvBeskrivelse.ControlToValidate = txtBeskrivelse.ID;
            rfvBeskrivelse.EnableClientScript = true;
            rfvBeskrivelse.ErrorMessage = @"Beskrivelse mangler!";
            rfvBeskrivelse.Text = @"*";
            rfvBeskrivelse.SetFocusOnError = true;

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

            rfvOverskrift.ControlToValidate = txtOverskrift.ID;
            rfvOverskrift.EnableClientScript = true;
            rfvOverskrift.ErrorMessage = @"Overskrift mangler!";
            rfvOverskrift.Text = @"*";
            rfvOverskrift.SetFocusOnError = true;
        }

        private void FillControls()
        {
            var ctrl = new OpslagController();
            ctrl.Get(_myConnection, User, drpOpslag);

            var item = new ListItem { Text = @"-- Vælg --", Value = @"0" };
            drpOpslag.Items.Insert(0, item);
            drpOpslag.SelectedIndex = 0;

            btnInsert.Enabled = true;
        }

        private void ClearControls()
        {
            txtBeskrivelse.Text = string.Empty;
            txtDato.Text = DateTimeHelper.DateToStringInGuiFormat(DateTime.Today);
            txtForfatter.Text = User.Identity.Name;
            txtId.Text = string.Empty;
            txtLink.Text = string.Empty;
            txtOverskrift.Text = string.Empty;

            rbnBilledLiggende.Checked = false;
            rbnBilledStaaende.Checked = false;

            chkOffentlig.Checked = false;

            btnDelete.Enabled = false;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;

            lblMessage.Text = string.Empty;
        }

        #endregion
    }
}
