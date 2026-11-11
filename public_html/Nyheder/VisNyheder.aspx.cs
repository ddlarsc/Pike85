using System;
using System.Collections.Generic;
using System.Web.UI;
using App_Code.Model;
using Control;
using UserControls;

namespace Nyheder
{
    public partial class VisNyheder : Page
    {
        private string _myConnection = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if (IsPostBack) { return; }

            HentNyheder();
        }

        private void HentNyheder()
        {
            var ctrl = new NyhedController();
            bool isAuthenticated = User.Identity.IsAuthenticated;

            List<NyhedObject> nyheder = ctrl.Get(_myConnection, isAuthenticated, 10);
            foreach (var nyhed in nyheder)
            {
                var ucNyhed = (UcNyheder)Page.LoadControl("~/UserControls/UcNyheder.ascx");
                ucNyhed.NyhedObject = nyhed;
                phNyheder.Controls.Add(ucNyhed);
            }
        }
    }
}