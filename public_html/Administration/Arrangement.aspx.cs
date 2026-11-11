using System.Globalization;
using App_Code;
using App_Code.Model;
using System;
using System.Web.UI.WebControls;
using Control;
using Model;

namespace Administration
{
    public partial class Arrangement:SkabelonPage
    {
        private string _myConnection;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender,EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _myConnection = Application["MySQLConnString"].ToString();
            lblMessages.Text = string.Empty;

            if(IsPostBack) { return; }

            try
            {
                EnableButtons(false);
                OnPageLoad();
                btnDelete.Attributes.Add("onclick","return confirm('Are you sure you want to delete this Arrangement?');");
            }
            catch(Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        protected void BtnCreateClick(object sender,EventArgs e)
        {
            try
            {
                OnCreateUpdate(EmailSubjectEnum.CreateArragement);
            }
            catch(Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        protected void BtnUpdateClick(object sender,EventArgs e)
        {
            try
            {
                OnCreateUpdate(EmailSubjectEnum.UpdateArrangement);
            }
            catch(Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        protected void BtnDeleteClick(object sender,EventArgs e)
        {
            try
            {
                OnDelete(EmailSubjectEnum.DeleteArrangement);
            }
            catch(Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        protected void DrpGetArrangementSelectedIndexChanged(object sender,EventArgs e)
        {
            try
            {
                OnDropdownlistSelect();
            }
            catch(Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        #region Private Members

        private void OnPageLoad()
        {
            //Handling WebControls on WebForm
            btnCreate.Enabled = true;
            btnDelete.CausesValidation = false;

            //Setting up the Calendar Icons
            pickDate1.Attributes.Add("onclick","cal1.select(" + IdentifyCtrl(txtDeadlineDate) + ",'" + IdentifyCtrl(anchorCal) + "','dd-MM-yyyy', getDate(" + IdentifyCtrl(txtDeadlineDate) + ")); return false;");
            pickDate1.Attributes.Add("src",Resources.Pike85.IconCalendarDay);
            pickDate2.Attributes.Add("onclick","cal1.select(" + IdentifyCtrl(txtStartDate) + ",'" + IdentifyCtrl(anchorCal) + "','dd-MM-yyyy', getDate(" + IdentifyCtrl(txtStartDate) + ")); return false;");
            pickDate2.Attributes.Add("src",Resources.Pike85.IconCalendarDay);
            pickDate3.Attributes.Add("onclick","cal1.select(" + IdentifyCtrl(txtEndDate) + ",'" + IdentifyCtrl(anchorCal) + "','dd-MM-yyyy', getDate(" + IdentifyCtrl(txtEndDate) + ")); return false;");
            pickDate3.Attributes.Add("src",Resources.Pike85.IconCalendarDay);

            //Private methods
            SetMailAddressTo();
            InitializeValidators();
            GetArrangements();
            InternalHelper.SetFocus(drpGetArrangement);
        }

        private void OnDelete(EmailSubjectEnum type)
        {
            var setupId = int.Parse(drpGetArrangement.SelectedValue);

            var ctrl = new AdminArrangementController();
            ctrl.Send(_myConnection, setupId, drpMailAddressTo.SelectedValue, type);
            ctrl.DeleteSetup(_myConnection, setupId);

            lblMessages.Text = @"Arrangementet er blevet slettet!";
            ResetPage();
            btnCreate.Enabled = true;
        }

        private void OnCreateUpdate(EmailSubjectEnum type)
        {
            if(IsValid)
            {
                var setupId = ObjectHelper.GetIntegerFromObject(drpGetArrangement.SelectedValue);
                var setup = new ArrangementSetupObject
                            {
                                Arrangements = null,
                                Deadline = DateTimeHelper.StringToDateInDanishFormat(txtDeadlineDate.Text),
                                End = DateTimeHelper.StringToDateInDanishFormat(txtEndDate.Text),
                                Header = txtOverskrift.Text,
                                Id = setupId,
                                Location = txtSted.Text,
                                Remarks = txtRemarks.Text,
                                Signature = User.Identity.Name,
                                Start = DateTimeHelper.StringToDateInDanishFormat(txtStartDate.Text),
                                TimeStamp = DateTime.Now,
                            };

                var ctrl = new AdminArrangementController();
                setupId = ctrl.SetSepcificSetup(_myConnection, setup);
                ctrl.Send(_myConnection, setupId, drpMailAddressTo.SelectedValue, type);

                lblMessages.Text = @"Arrangementet er nu oprettet/opdateret!";
                ResetPage();
                btnCreate.Enabled = true;
            }
        }

        private void OnDropdownlistSelect()
        {
            ClearCtrl(true);
            EnableButtons(false);

            if(drpGetArrangement.SelectedIndex < 1)
            {
                btnCreate.Enabled = true;
            }
            else
            {
                FillControls();
            }
        }

        private static string IdentifyCtrl(System.Web.UI.Control ctrl)
        {
            return ctrl.UniqueID.Replace(":","_");
        }

        private void EnableButtons(bool enable)
        {
            btnCreate.Enabled = (drpGetArrangement.SelectedIndex < 1);
            btnDelete.Enabled = enable;
            btnUpdate.Enabled = enable;
        }

        private void ResetPage()
        {
            ClearCtrl(false);
            GetArrangements();
            InternalHelper.SetFocus(drpGetArrangement);
        }

        private void FillControls()
        {
            var setupId = int.Parse(drpGetArrangement.SelectedValue);

            var ctrl = new AdminArrangementController();
            ArrangementSetupObject setup = ctrl.GetAllParticipantsForSpecificSetup(_myConnection,setupId);

            txtId.Text = setup.Id.ToString(CultureInfo.InvariantCulture);
            txtOverskrift.Text = setup.Header;
            txtSted.Text = setup.Location;
            txtRemarks.Text = setup.Remarks;
            txtDeadlineDate.Text = DateTimeHelper.DateToStringInGuiFormat(setup.Deadline);
            txtStartDate.Text = DateTimeHelper.DateToStringInGuiFormat(setup.Start);
            txtEndDate.Text = DateTimeHelper.DateToStringInGuiFormat(setup.End);

            var now = DateTime.Now;
            if(DateTime.Compare(now, setup.Start) < 0)
            {
                btnUpdate.Enabled = true;
            }
            if(DateTime.Compare(now, setup.End) < 0 && setup.Arrangements.Count == 0)
            {
                btnDelete.Enabled = true;
            }
        }

        private void ClearCtrl(bool clearMessages)
        {
            txtOverskrift.Text = string.Empty;
            txtSted.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtDeadlineDate.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            if(clearMessages)
            {
                lblMessages.Text = string.Empty;
            }
        }

        private void SetMailAddressTo()
        {
            ListItem[] validValues = EnumHelper.GetNameDescriptionPair(typeof(MailAddressTo));
            drpMailAddressTo.Items.AddRange(validValues);
            drpMailAddressTo.SelectedIndex = -1;
        }

        private void GetArrangements()
        {
            drpGetArrangement.Items.Clear();

            var ctrl = new AdminArrangementController();
            ctrl.GetSpecificParticipantForAllSetups(_myConnection, User, drpGetArrangement);

            var item = new ListItem { Text = @"-- Vælg --",Value = @"0" };
            drpGetArrangement.Items.Insert(0,item);
            drpGetArrangement.SelectedIndex = -1;
        }

        private void InitializeValidators()
        {
            //Textfields
            rfvHeader.ControlToValidate = txtOverskrift.ID;
            rfvHeader.EnableClientScript = true;
            rfvHeader.ErrorMessage = @"Overskrift må ikke være tomt!";
            rfvHeader.Text = @"*";
            rfvHeader.SetFocusOnError = true;

            rfvLocation.ControlToValidate = txtSted.ID;
            rfvLocation.EnableClientScript = true;
            rfvLocation.ErrorMessage = @"Sted må ikke være tomt!";
            rfvLocation.Text = @"*";
            rfvLocation.SetFocusOnError = true;

            rfvRemarks.ControlToValidate = txtRemarks.ID;
            rfvRemarks.EnableClientScript = true;
            rfvRemarks.ErrorMessage = @"Bemærkninger må ikke være tomt!";
            rfvRemarks.Text = @"*";
            rfvRemarks.SetFocusOnError = true;

            //DeadlineDate
            rfvDeadlineDate.ControlToValidate = txtDeadlineDate.ID;
            rfvDeadlineDate.EnableClientScript = true;
            rfvDeadlineDate.ErrorMessage = @"Deadline må ikke være tomt!";
            rfvDeadlineDate.Text = @"*";
            rfvDeadlineDate.SetFocusOnError = true;

            revDeadlineDate.ControlToValidate = txtDeadlineDate.ID;
            revDeadlineDate.EnableClientScript = true;
            revDeadlineDate.ErrorMessage = @"Deadline dato skal være DD-MM-ÅÅÅÅ format!";
            revDeadlineDate.SetFocusOnError = true;
            revDeadlineDate.Text = @"!";
            revDeadlineDate.ValidationExpression = "\\d{2}-\\d{2}-\\d{4}";

            //Startdate
            rfvStartDate.ControlToValidate = txtStartDate.ID;
            rfvStartDate.EnableClientScript = true;
            rfvStartDate.ErrorMessage = @"Startdato må ikke være tomt!";
            rfvStartDate.Text = @"*";
            rfvStartDate.SetFocusOnError = true;

            revStartDate.ControlToValidate = txtStartDate.ID;
            revStartDate.EnableClientScript = true;
            revStartDate.ErrorMessage = @"Start dato skal være DD-MM-ÅÅÅÅ format!";
            revStartDate.SetFocusOnError = true;
            revStartDate.Text = @"!";
            revStartDate.ValidationExpression = "\\d{2}-\\d{2}-\\d{4}";

            cvStartDate.ControlToCompare = txtDeadlineDate.ID;
            cvStartDate.ControlToValidate = txtStartDate.ID;
            cvStartDate.CultureInvariantValues = true;
            cvStartDate.Display = ValidatorDisplay.Dynamic;
            cvStartDate.EnableClientScript = true;
            cvStartDate.ErrorMessage = @"Startdato skal kommer efter deadline!";
            cvStartDate.Operator = ValidationCompareOperator.GreaterThan;
            cvStartDate.SetFocusOnError = true;
            cvStartDate.Text = @"!";
            cvStartDate.Type = ValidationDataType.Date;

            //Enddate
            rfvEndDate.ControlToValidate = txtEndDate.ID;
            rfvEndDate.EnableClientScript = true;
            rfvEndDate.ErrorMessage = @"Slutdato må ikke være tomt!";
            rfvEndDate.Text = @"*";
            rfvEndDate.SetFocusOnError = true;

            revEndDate.ControlToValidate = txtEndDate.ID;
            revEndDate.EnableClientScript = true;
            revEndDate.ErrorMessage = @"Slut dato skal være DD-MM-ÅÅÅÅ format!";
            revEndDate.SetFocusOnError = true;
            revEndDate.Text = @"*";
            revEndDate.ValidationExpression = "\\d{2}-\\d{2}-\\d{4}";

            cvEndDate.ControlToCompare = txtStartDate.ID;
            cvEndDate.ControlToValidate = txtEndDate.ID;
            cvEndDate.CultureInvariantValues = true;
            cvEndDate.Display = ValidatorDisplay.Dynamic;
            cvEndDate.EnableClientScript = true;
            cvEndDate.ErrorMessage = @"Slutdato skal være lig med eller større end startdato!";
            cvEndDate.Operator = ValidationCompareOperator.GreaterThanEqual;
            cvEndDate.SetFocusOnError = true;
            cvEndDate.Text = @"!";
            cvEndDate.Type = ValidationDataType.Date;

            //Summary
            vldSummery.DisplayMode = ValidationSummaryDisplayMode.BulletList;
            vldSummery.EnableClientScript = true;
            vldSummery.HeaderText = @"Oprettelse af et arrangement er afbrudt! Der er en eller flere fejl.";
            vldSummery.ShowSummary = true;
        }

        #endregion
    }
}