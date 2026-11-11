using System.Web.UI.WebControls;
using App_Code;
using System;
using System.Text;
using Control;

namespace Medlemmer.Arrangement
{
    public partial class View:SkabelonPage
    {
        private string _myConnection;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender,EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if(!IsPostBack)
            {
                OnPageLoad();
            }
        }

        protected void DrpSelectedIndexChanged(object sender,EventArgs e)
        {
            OnDropdownlistSelect();
        }

        protected string GetImageUrl(string arg)
        {
            var idx = int.Parse(arg);
            switch(idx)
            {
                case 0:
                    {
                        return Resources.Pike85.IconGreen;
                    }
                case 1:
                    {
                        return Resources.Pike85.IconRed;
                    }
                default:
                    {
                        return Resources.Pike85.IconYellow;
                    }
            }
        }

        protected string SetDayAndPeriode(string arg1,string arg2)
        {
            var result = new StringBuilder();
            if(!string.IsNullOrEmpty(arg1))
            {
                result.Append(DateTime.Parse(arg1).DayOfWeek);
            }
            if(!string.IsNullOrEmpty(arg2))
            {
                result.AppendFormat(" ({0})",arg2);
            }
            return result.ToString();
        }

        #region Private Methods

        private void OnPageLoad()
        {
            try
            {
                var ctrl = new ArrangementController();
                ctrl.GetAllParticipantsForSpecificSetup(_myConnection,drpArrangement);

                var item = new ListItem { Text = @"-- Vælg --",Value = @"-- Vælg --" };
                drpArrangement.Items.Insert(0,item);
                drpArrangement.SelectedIndex = -1;

                InternalHelper.SetFocus(drpArrangement);

                var id = Request.QueryString.Get("id");
                if(id != "")
                {
                    drpArrangement.SelectedValue = id;
                    DrpSelectedIndexChanged(null,null);
                }
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void OnDropdownlistSelect()
        {
            if(drpArrangement.SelectedIndex > 0)
            {
                try
                {
                    var ctrl = new ArrangementController();
                    ctrl.GetAllParticipantsForSpecificSetup(_myConnection,drpArrangement,dgArrangementer);

                    var setupId = ObjectHelper.GetIntegerFromObject(drpArrangement.SelectedValue);
                    lblInfo.Text = ctrl.Info(_myConnection,setupId);
                }
                catch(Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
        }

        #endregion
    }
}