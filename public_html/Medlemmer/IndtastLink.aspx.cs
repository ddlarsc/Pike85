using System;
using System.Globalization;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;
using Control;

namespace Medlemmer
{
    public partial class MedlemmerIndtastLink : System.Web.UI.Page
    {
        private string _myConnection;

        protected void Page_Load(object sender, EventArgs e)
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

        protected void DrpLinkSelectedIndexChanged(object sender,EventArgs e)
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
                OnInsertAndUpdate();
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
                OnInsertAndUpdate();
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
            ValidateControls();
            ClearControls();
            FillDropdownlists();
            btnInsert.Enabled = true;

            drpLink.Focus();
            btnDelete.Attributes.Add("onclick","return confirm('Er du sikker på, at du vil slette dette link?');");
        }

        private void OnIndexChange()
        {
            lblMessage.Text = string.Empty;
            if(drpLink.SelectedIndex < 1)
            {
                ClearControls();
                btnInsert.Enabled = true;
                return;
            }
            LoadControls(Convert.ToInt32(drpLink.SelectedValue));
        }

        private void OnInsertAndUpdate()
        {
            lblMessage.Text = string.Empty;
            if(IsValid)
            {
                string filnavn = string.Empty;
                if(fuBilled.HasFile)
                {
                    string billedMappe = "/Billeder/Link/" + DateTime.Now.Year + "/";
                    filnavn = InternalHelper.OpretBilledFiler(Page.Server.MapPath(billedMappe), fuBilled, rbnBilledStaaende.Checked);
                }
                
                var ctrl = new LinkController();
                if(string.IsNullOrEmpty(txtId.Text))
                {
                    var link = new LinkObject(0,User.Identity.Name,txtNavn.Text,txtAdresse.Text,txtBeskrivelse.Text,drpKategori.SelectedValue,drpLand.SelectedValue,filnavn);
                    var id = ctrl.Insert(_myConnection, link);
                    FillDropdownlists();
                    LoadControls(id);
                }
                else
                {
                    var link = new LinkObject(Convert.ToInt32(txtId.Text),txtForfatter.Text,txtNavn.Text,txtAdresse.Text,txtBeskrivelse.Text,drpKategori.SelectedValue,drpLand.SelectedValue,filnavn);
                    ctrl.Update(_myConnection, link);
                }
                
                lblMessage.Text = @"Data er gemt!";
            }
        }

        private void OnDelete()
        {
            var ctrl = new LinkController();
            ctrl.Delete(_myConnection, Convert.ToInt32(txtId.Text));
            ClearControls();
            FillDropdownlists();
            btnInsert.Enabled = true;

            lblMessage.Text = @"Data er slettet!";
        }

        private void ValidateControls()
        {
            rfvAdresse.ControlToValidate = txtAdresse.ID;
            rfvAdresse.EnableClientScript = true;
            rfvAdresse.ErrorMessage = @"Adresse mangler!";
            rfvAdresse.Text = @"*";
            rfvAdresse.SetFocusOnError = true;

            rfvBeskrivelse.ControlToValidate = txtBeskrivelse.ID;
            rfvBeskrivelse.EnableClientScript = true;
            rfvBeskrivelse.ErrorMessage = @"Beskrivelse mangler!";
            rfvBeskrivelse.Text = @"*";
            rfvBeskrivelse.SetFocusOnError = true;

            rfvKategori.ControlToValidate = drpKategori.ID;
            rfvKategori.EnableClientScript = true;
            rfvKategori.ErrorMessage = @"kategori mangler!";
            rfvKategori.InitialValue = EnumHelper.GetDescription(LinkKategoriEnum.None);
            rfvKategori.Text = @"*";
            rfvKategori.SetFocusOnError = true;

            rfvLand.ControlToValidate = drpLand.ID;
            rfvLand.EnableClientScript = true;
            rfvLand.ErrorMessage = @"Land mangler!";
            rfvLand.InitialValue = EnumHelper.GetDescription(LinkLandEnum.None);
            rfvLand.Text = @"*";
            rfvLand.SetFocusOnError = true;

            rfvNavn.ControlToValidate = txtNavn.ID;
            rfvNavn.EnableClientScript = true;
            rfvNavn.ErrorMessage = @"Navn mangler!";
            rfvNavn.Text = @"*";
            rfvNavn.SetFocusOnError = true;
        }

        private void FillDropdownlists()
        {
            var ctrl = new LinkController();
            ctrl.Get(_myConnection, User, drpLink);

            var item = new ListItem { Text = @"-- Vælg --",Value = @"0" };
            drpLink.Items.Insert(0,item);
            drpLink.SelectedIndex = 0;

            Getkategori(drpKategori,typeof(LinkKategoriEnum));
            Getkategori(drpLand,typeof(LinkLandEnum));
        }

        private void ClearControls()
        {
            drpLink.SelectedIndex = -1;
            txtId.Text = string.Empty;
            txtForfatter.Text = User.Identity.Name;
            txtNavn.Text = string.Empty;
            txtAdresse.Text = string.Empty;
            txtBeskrivelse.Text = string.Empty;
            drpKategori.SelectedIndex = -1;
            drpLand.SelectedIndex = -1;
            fuBilled.Dispose();

            btnDelete.Enabled = false;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void LoadControls(int id)
        {
            if(id == 0) return;

            var ctrl = new LinkController();
            LinkObject link = ctrl.Get(_myConnection, id);

            txtAdresse.Text = link.Adresse;
            txtBeskrivelse.Text = link.Beskrivelse;
            txtForfatter.Text = link.Forfatter;
            txtId.Text = link.Id.ToString(CultureInfo.InvariantCulture);
            drpKategori.SelectedValue = link.Kategori;
            drpLand.SelectedValue = link.Land;
            txtNavn.Text = link.Navn;
            //Todo - load fuBillede, hvis det er muligt?

            btnInsert.Enabled = false;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void Getkategori(DropDownList dropDownList, Type type)
        {
            ListItem[] descriptionNamePair = EnumHelper.GetDescriptionNamePair(type);
            dropDownList.Dispose();
            dropDownList.DataSource = descriptionNamePair;
            dropDownList.DataBind();
        }

        #endregion
    }
}
