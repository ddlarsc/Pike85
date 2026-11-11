using System;
using System.Web.UI.WebControls;
using App_Code.Model;
using Control;

namespace Medlemmer
{
    public partial class IndtastArt: System.Web.UI.Page
    {
        string _myConnection;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender,EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if(IsPostBack) { return; }

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
            ClearControls();
            InitControls();
            InitializeValidators();
            
            btnInsert.Enabled = true;
            btnDelete.Attributes.Add("onclick","return confirm('Er du sikker på, at du vil slette denne art?');");
        }

        private void OnSelectedIndexChanged()
        {
            ClearControls();
            if(drpArt.SelectedIndex < 1)
            {
                InitControls();
                return;
            }

            txtArtId.Text = drpArt.SelectedItem.Value;
            txtArtNavn.Text = drpArt.SelectedItem.Text;

            var presentSelectedArt = new ArterObject(drpArt.SelectedIndex, drpArt.SelectedItem.Text);
            var countUseOfArt = new FangsterController().CountUseOfArt(_myConnection, presentSelectedArt);
            if(countUseOfArt == 0)
            {
                txtArtNavn.Enabled = true;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
            else
            {
                txtArtNavn.Enabled = false;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }

            drpArt.Focus();
        }

        private void OnInsertUpdate()
        {
            lblMessage.Text = string.Empty;
            if(IsValid)
            {
                var presentSelectedArtId = Convert.ToInt32(drpArt.SelectedItem.Value);
                if(presentSelectedArtId == 0)
                {
                    var ctrl = new ArterController();
                    var nextId = ctrl.NextId(_myConnection);
                    var art = new ArterObject(nextId, txtArtNavn.Text);
                    ctrl.Insert(_myConnection,art);
                }
                else
                {
                    var art = new ArterObject(presentSelectedArtId, txtArtNavn.Text);
                    var ctrl = new ArterController();
                    ctrl.Update(_myConnection,art);
                }

                ClearControls();
                InitControls();
                lblMessage.Text = @"Data er gemt!";
            }
        }

        private void OnDelete()
        {
            var art = new ArterObject(Convert.ToInt32(drpArt.SelectedItem.Value), drpArt.SelectedItem.Text);
            var ctrl = new ArterController();
            ctrl.Delete(_myConnection, art);

            ClearControls();
            InitControls();

            lblMessage.Text = @"Data er slettet!";
        }

        private void InitializeValidators()
        {
            rfvArtNavn.ControlToValidate = txtArtNavn.ID;
            rfvArtNavn.EnableClientScript = true;
            rfvArtNavn.ErrorMessage = @"Art mangler!";
            rfvArtNavn.Text = @"*";
            rfvArtNavn.SetFocusOnError = true;

            //Summary
            vsMessage.DisplayMode = ValidationSummaryDisplayMode.BulletList;
            vsMessage.EnableClientScript = true;
            vsMessage.HeaderText = @"Oprettelse af ART er afbrudt! Der er en eller flere fejl.";
            vsMessage.ShowSummary = true;
        }

        private void InitControls()
        {
            FillHeader();

            txtArtId.Text = @"0";
            txtArtNavn.Text = string.Empty;
            btnInsert.Enabled = true;
            drpArt.Focus();
        }

        private void FillHeader()
        {
            var ctrl = new ArterController();
            ctrl.Get(_myConnection, drpArt);

            var item = new ListItem { Text = @"-- Vælg --", Value = @"0" };
            drpArt.Items.Insert(0,item);
            drpArt.SelectedIndex = 0;
        }

        private void ClearControls()
        {
            txtArtId.Text = string.Empty;
            txtArtNavn.Text = string.Empty;

            btnDelete.Enabled = false;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;

            lblMessage.Text = string.Empty;
        }

        #endregion
    }
}
