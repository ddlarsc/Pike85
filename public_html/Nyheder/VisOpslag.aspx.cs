using System;
using System.Collections.Generic;
using System.Web.UI;
using App_Code.Model;
using Control;
using UserControls;

namespace Nyheder
{
    public partial class VisOpslag: Page
    {
        private string _myConnection = string.Empty;
        
        protected void Page_Load(object sender,EventArgs e)
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if(IsPostBack) { return; }

            HentOpslag();
        }

        private void HentOpslag()
        {
            var ctrl = new OpslagController();
            bool isAuthenticated = User.Identity.IsAuthenticated;
            
            IEnumerable<OpslagObject> opslagCollection = ctrl.Get(_myConnection, isAuthenticated, 10);
            foreach(var opslag in opslagCollection)
            {
                var ucOpslag = (UcOpslag)Page.LoadControl("~/UserControls/UcOpslag.ascx");
                ucOpslag.Opslag = opslag;
                phOpslag.Controls.Add(ucOpslag);
            }
        }
    }
}
