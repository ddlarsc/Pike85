using System.Globalization;
using App_Code;
using App_Code.Model;
using System;
using System.Web.UI.WebControls;
using Control;
using Model;

namespace Medlemmer
{
    public partial class IndtastArrangement : SkabelonPage
    {
        private string _myConnection;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _myConnection = Application["MySQLConnString"].ToString();
            btnCommit.Enabled = false;

            if (!IsPostBack)
            {
                OnPageLoad();
            }
            lblMessage.Text = string.Empty;
        }

        protected void BtnCommitClick(object sender, EventArgs e)
        {
            OnButtonClick();
        }

        protected void DrpArrangementSelectedIndexChanged(object sender, EventArgs e)
        {
            OnDropdownlistSelect();
        }

        protected void RblAnticipateSelectedIndexChanged(object sender, EventArgs e)
        {
            OnRadiobuttonClick();
        }

        #region Private Members

        private void OnPageLoad()
        {
            try
            {
                ResetControls(false);
                var ctrl = new ArrangementController();
                ctrl.GetAllSetups(_myConnection, drpSelectArrangement);

                var item = new ListItem { Text = @"-- Vælg --", Value = @"-- Vælg --" };
                drpSelectArrangement.Items.Insert(0, item);
                drpSelectArrangement.SelectedIndex = 0;

                InitiateDropdownList();
                InitiateValidators();
                EnableValidators(true);

                InternalHelper.SetFocus(drpSelectArrangement);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void OnButtonClick()
        {
            try
            {
                string name = User.Identity.Name;

                if (drpSelectArrangement.SelectedIndex < 1 || string.IsNullOrEmpty(name) || !IsValid)
                {
                    return;
                }

                var id = int.Parse(lblUniqueKey.Text);
                var decline = int.Parse(rblAnticipate.SelectedValue);
                var setupId = int.Parse(drpSelectArrangement.SelectedValue);
                var arrivelDate = DateTimeHelper.StringToDateInDanishFormat(drpArrivalDate.SelectedValue);
                var arrivalOnDay = drpArrivalOnDay.SelectedValue;
                var departureDate = DateTimeHelper.StringToDateInDanishFormat(drpDepartureDate.SelectedValue);
                var departureOnDay = drpDepartureOnDay.SelectedValue;
                var message = txtMessageAny.Text;
                var messageAboutBoat = txtMessageBoat.Text;
                var messageAboutBed = txtMessageBed.Text;

                var arrangementObject = new ArrangementObject(id, name, setupId, decline, arrivelDate, arrivalOnDay, departureDate, departureOnDay, message, messageAboutBoat, messageAboutBed);
                var ctrl = new ArrangementController();
                ctrl.SetSpecificParticipant(_myConnection, arrangementObject);

                lblMessage.Text = @"Dit svar er gemt!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void OnDropdownlistSelect()
        {
            ResetControls(false);
            if (drpSelectArrangement.SelectedIndex > 0)
            {
                ResetControls(true);
                int idx = Int16.Parse(drpSelectArrangement.SelectedValue);
                if (idx != 0)
                {
                    drpArrivalDate.Items.Clear();
                    drpDepartureDate.Items.Clear();
                    InitiateDropdownList();
                    FillControls(idx, User.Identity.Name);
                }
            }
        }

        private void OnRadiobuttonClick()
        {
            EnableValidators(rblAnticipate.SelectedValue == "0");
            btnCommit.Enabled = false;
            if (rblAnticipate.SelectedIndex > -1)
            {
                btnCommit.Enabled = true;
            }
        }

        private void EnableValidators(bool enable)
        {
            rfvldArrivalDate.Enabled = enable;
            rvArrivalOnDay.Enabled = enable;
            rfvldDepartureDate.Enabled = enable;
            rvDepartureOnDay.Enabled = enable;
        }

        private void InitiateValidators()
        {
            rfvldArrivalDate.ControlToValidate = drpArrivalDate.ID;
            rfvldArrivalDate.ErrorMessage = @"Du skal vælge en start dato!";
            rfvldArrivalDate.InitialValue = drpArrivalDate.SelectedValue;
            rfvldArrivalDate.SetFocusOnError = true;
            rfvldArrivalDate.Text = @"*";

            rvArrivalOnDay.ControlToValidate = drpArrivalOnDay.ID;
            rvArrivalOnDay.ErrorMessage = @"Du skal vælge et tidspunkt på dagen.";
            rvArrivalOnDay.InitialValue = drpArrivalOnDay.SelectedValue;
            rvArrivalOnDay.SetFocusOnError = true;
            rvArrivalOnDay.Text = @"*";

            rfvldDepartureDate.ControlToValidate = drpDepartureDate.ID;
            rfvldDepartureDate.ErrorMessage = @"Du skal vælge en slut dato!";
            rfvldDepartureDate.InitialValue = drpDepartureDate.SelectedValue;
            rfvldDepartureDate.SetFocusOnError = true;
            rfvldDepartureDate.Text = @"*";

            rvDepartureOnDay.ControlToValidate = drpDepartureOnDay.ID;
            rvDepartureOnDay.ErrorMessage = @"Du skal vælge et tidspunkt på dagen.";
            rvDepartureOnDay.InitialValue = drpDepartureOnDay.SelectedValue;
            rvDepartureOnDay.SetFocusOnError = true;
            rvDepartureOnDay.Text = @"*";
        }

        private void ResetControls(bool enable)
        {
            rblAnticipate.Enabled = enable;
            rblAnticipate.SelectedIndex = -1;

            drpArrivalDate.Enabled = enable;
            drpArrivalDate.SelectedIndex = -1;

            drpArrivalOnDay.Enabled = enable;
            drpArrivalOnDay.SelectedIndex = -1;

            drpDepartureDate.Enabled = enable;
            drpDepartureDate.SelectedIndex = -1;

            drpDepartureOnDay.Enabled = enable;
            drpDepartureOnDay.SelectedIndex = -1;

            txtMessageAny.Enabled = enable;
            txtMessageAny.Text = string.Empty;

            txtMessageBed.Enabled = enable;
            txtMessageBoat.Text = string.Empty;

            txtMessageBoat.Enabled = enable;
            txtMessageBed.Text = string.Empty;
        }

        private void FillControls(int id, string name)
        {
            try
            {
                var ctrl = new ArrangementController();
                ArrangementSetupObject setup = ctrl.GetSpecificParticipantForSpecificSetup(_myConnection, id, name);
                if (DateTime.Now.CompareTo(setup.Deadline) > 0)
                {
                    lblMessage.Text = @"Deadline er overskredet!";
                }

                DateTime start = setup.Start;
                while (start <= setup.End)
                {
                    var text = DateTimeHelper.DateToStringInGuiFormat(start) + @" (" + start.DayOfWeek + @")";
                    var value = DateTimeHelper.DateToStringInGuiFormat(start);

                    drpArrivalDate.Items.Add(new ListItem { Text = text, Value = value });
                    drpDepartureDate.Items.Add(new ListItem { Text = text, Value = value });

                    start = start.AddDays(1);
                }

                lblUniqueKey.Text = @"0";
                SetRadiobuttonList(rblAnticipate, int.MinValue);
                SetDropdownlistIndex(drpArrivalDate, string.Empty);
                SetDropdownlistIndex(drpArrivalOnDay, string.Empty);
                SetDropdownlistIndex(drpDepartureDate, string.Empty);
                SetDropdownlistIndex(drpDepartureOnDay, string.Empty);

                if (setup.Arrangements.Count > 0)
                {
                    lblUniqueKey.Text = setup.Arrangements[0].Id.ToString(CultureInfo.InvariantCulture);
                    SetRadiobuttonList(rblAnticipate, setup.Arrangements[0].Decline);

                    string arrivalDate = DateTimeHelper.DateToStringInGuiFormat(setup.Arrangements[0].ArrivelDate);
                    SetDropdownlistIndex(drpArrivalDate, arrivalDate);

                    drpArrivalOnDay.SelectedValue = setup.Arrangements[0].ArrivalOnDay;

                    string departureDate = DateTimeHelper.DateToStringInGuiFormat(setup.Arrangements[0].DepartureDate);
                    SetDropdownlistIndex(drpDepartureDate, departureDate);

                    drpDepartureOnDay.SelectedValue = setup.Arrangements[0].DepartureOnDay;

                    txtMessageAny.Text = setup.Arrangements[0].Message;
                    txtMessageBoat.Text = setup.Arrangements[0].MessageAboutBoat;
                    txtMessageBed.Text = setup.Arrangements[0].MessageAboutBed;

                    btnCommit.Enabled = true;
                }
                BetweenDeadlineAndStartdate(setup);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void InitiateDropdownList()
        {
            drpArrivalOnDay.Items.Clear();
            drpDepartureOnDay.Items.Clear();

            ListItem[] arrival = EnumHelper.GetDescriptionNamePair(typeof(ArrangementEnum));
            drpArrivalOnDay.Items.AddRange(arrival);

            ListItem[] departure = EnumHelper.GetDescriptionNamePair(typeof(ArrangementEnum));
            drpDepartureOnDay.Items.AddRange(departure);
        }

        private static void SetDropdownlistIndex(DropDownList ctrl, string selectedValue)
        {
            ctrl.SelectedIndex = -1;
            if (selectedValue == "")
                return;

            for (int idx = 0; idx < ctrl.Items.Count; idx++)
            {
                ctrl.SelectedIndex = idx;
                if (ctrl.SelectedValue == selectedValue)
                {
                    return;
                }
            }
        }

        private void SetRadiobuttonList(RadioButtonList radioButtonList, int selectedValue)
        {
            radioButtonList.SelectedIndex = -1;
            if (selectedValue == int.MinValue) { return; }

            for (int idx = 0; idx < radioButtonList.Items.Count; idx++)
            {
                radioButtonList.SelectedIndex = idx;
                if (radioButtonList.SelectedValue == selectedValue.ToString(CultureInfo.InvariantCulture))
                {
                    return;
                }
            }
        }

        private void BetweenDeadlineAndStartdate(ArrangementSetupObject setup)
        {
            if (DateTime.Now.CompareTo(setup.Deadline) > 0 && DateTime.Now.CompareTo(setup.Start) < 0)
            {
                rblAnticipate.Enabled = false;
                drpArrivalDate.Enabled = false;
                drpArrivalOnDay.Enabled = false;
                drpDepartureDate.Enabled = false;
                drpDepartureOnDay.Enabled = false;
            }
        }

        #endregion Private Members
    }
}