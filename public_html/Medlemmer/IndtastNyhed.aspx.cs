using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;
using Control;

namespace Medlemmer
{
    public partial class IndtastNyhed : Page
    {
        string _myConnection;
        protected void Page_Load(object sender, EventArgs e)
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if(IsPostBack) { return; }

            try
            {
                OnpageLoad();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void DrpNyhedSelectedIndexChanged(object sender,EventArgs e)
        {
            try
            {
                OnIndexChange();
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

        private void OnpageLoad()
        {
            LoadDropdownlist();
            ValidateControls();
            ClearControls();

            pickDate1.Attributes.Add("onclick","cal1.select(" + IdentifyCtrl(txtDato) + ",'" + IdentifyCtrl(anchorCal) + "','dd-MM-yyyy', getDate(" + IdentifyCtrl(txtDato) + ")); return false;");
            pickDate1.Attributes.Add("src", Resources.Pike85.IconCalendarDay);
            
            btnInsert.Enabled = true;
            drpNyhed.Focus();
            btnDelete.Attributes.Add("onclick","return confirm('Er du sikker på, at du vil slette denne nyhed?');");
        }

        private void OnIndexChange()
        {
            lblMessage.Text = string.Empty;
            if(drpNyhed.SelectedIndex < 1)
            {
                ClearControls();
                btnInsert.Enabled = true;
                return;
            }
            LoadControls(Convert.ToInt32(drpNyhed.SelectedValue));
        }

        private void OnInsertUpdate()
        {
            if(IsValid)
            {
                string billedMappe = string.Format("/Billeder/Nyheder/{0}/", DateTimeHelper.StringToDateInDanishFormat(txtDato.Text).Year);
                string filnavn1 = String.Empty;
                string filnavn2 = String.Empty;
                string filnavn3 = String.Empty;

                if(fuBilled1.HasFile)
                {
                    filnavn1 = InternalHelper.OpretBilledFiler(Page.Server.MapPath(billedMappe), fuBilled1, rbBilled1Portrait.Checked);
                }

                if(fuBilled2.HasFile)
                {
                    filnavn2 = InternalHelper.OpretBilledFiler(Page.Server.MapPath(billedMappe), fuBilled2, rbBilled2Portrait.Checked);
                }

                if(fuBilled3.HasFile)
                {
                    filnavn3 = InternalHelper.OpretBilledFiler(Page.Server.MapPath(billedMappe), fuBilled3, rbBilled3Portrait.Checked);
                }

                var ctrl = new NyhedController();
                if(txtId.Text == string.Empty)
                {
                    ctrl.Insert(_myConnection, new NyhedObject(0, txtForfatter.Text, DateTimeHelper.StringToDateInDanishFormat(txtDato.Text), txtOverskrift.Text, txtBeskrivelse.Text, txtLink.Text, billedMappe, filnavn1, filnavn2, filnavn3, string.Empty, string.Empty, string.Empty, chkOffentlig.Checked));
                }
                else
                {
                    ctrl.Update(_myConnection, new NyhedObject(Convert.ToInt32(txtId.Text), txtForfatter.Text, DateTimeHelper.StringToDateInDanishFormat(txtDato.Text), txtOverskrift.Text, txtBeskrivelse.Text, txtLink.Text, billedMappe, filnavn1, filnavn2, filnavn3, string.Empty, string.Empty, string.Empty, chkOffentlig.Checked));
                }

                LoadDropdownlist();
                lblMessage.Text = @"Data er gemt!";
            }
        }

        private void OnDelete()
        {
            var ctrl = new NyhedController();
            ctrl.Delete(_myConnection, Convert.ToInt32(drpNyhed.SelectedValue));

            LoadDropdownlist();
            ClearControls();
            lblMessage.Text = @"Data er slettet!";
        }

        private void ValidateControls()
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

        private void LoadDropdownlist()
        {
            var ctrl = new NyhedController();
            ctrl.Get(_myConnection, User, drpNyhed);

            var item = new ListItem { Text = @"-- Vælg --",Value = @"0" };
            drpNyhed.Items.Insert(0,item);
            drpNyhed.SelectedIndex = 0;
        }

        private void ClearControls()
        {
            drpNyhed.SelectedIndex = -1;
            
            txtId.Text = string.Empty;
            txtForfatter.Text = User.Identity.Name;
            txtDato.Text = string.Empty;
            txtOverskrift.Text = string.Empty;
            txtBeskrivelse.Text = string.Empty;
            txtLink.Text = string.Empty;
            
            fuBilled1.Dispose();
            rbBilled1Landscape.Checked = true;
            
            fuBilled2.Dispose();
            rbBilled2Landscape.Checked = true;
            
            fuBilled3.Dispose();
            rbBilled3Landscape.Checked = true;
            
            chkOffentlig.Checked = false;

            btnDelete.Enabled = false;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void LoadControls(int id)
        {
            if(id == 0) return;

            var ctrl = new NyhedController();
            var nyhed = ctrl.Get(_myConnection,id);

            txtId.Text = nyhed.Id.ToString(CultureInfo.InvariantCulture);
            txtForfatter.Text = nyhed.Forfatter;
            txtDato.Text = DateTimeHelper.DateToStringInGuiFormat(nyhed.Dato);
            txtOverskrift.Text = nyhed.Overskrift;
            txtBeskrivelse.Text = nyhed.Beskrivelse;
            txtLink.Text = nyhed.Link;
            //Todo - load FuBillede, hvis det er muligt?
            chkOffentlig.Checked = nyhed.Offentlig;

            btnInsert.Enabled = false;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private static string IdentifyCtrl(System.Web.UI.Control ctrl)
        {
            return ctrl.UniqueID.Replace(":","_");
        }

        #endregion
    }
}
