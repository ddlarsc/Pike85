using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Control;

namespace Fangster
{
    public partial class FindFangster : Page
    {
        private string _myConnection = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if(Page.IsPostBack) return;

            OnPageLoad();
        }

        protected void BtnGetClick(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                GetReports(drpPerson.SelectedValue, drpArt.SelectedValue, Convert.ToInt32(drpAntal.SelectedValue));
            }
        }

        private void OnPageLoad()
        {
            var brugerCtrl = new BrugerController();
            brugerCtrl.GetByIdAndKortnavn(_myConnection, drpPerson);
            var item = new ListItem { Text = @"-- KLUBBEN --", Value = @"" };
            drpPerson.Items.Insert(0, item);
            drpPerson.SelectedIndex = 0;

            var artCtrl = new ArterController();
            artCtrl.Get(_myConnection, drpArt);
            
            drpAntal.Items.Insert(0, "10");
            drpAntal.Items.Insert(1, "25");
            drpAntal.Items.Insert(2, "50");
            drpAntal.Items.Insert(3, "75");
            drpAntal.Items.Insert(4, "100");
            drpAntal.SelectedIndex = 0;
        }

        private void GetReports(string brugerId, string artId, int antal)
        {
            var ctrl = new FangsterController();
            InternalHelper.DataGridReader(_myConnection, ctrl.GetList(brugerId, antal, artId, true), dgDanmark);
            InternalHelper.DataGridReader(_myConnection, ctrl.GetList(brugerId, antal, artId, false), dgUdland);
        }
    }
}