
using System;
using System.Collections.Generic;
using System.Text;
using App_Code;
using App_Code.Model;
using Control;

namespace UserControls
{
    public partial class UcDefaultOpslag: System.Web.UI.UserControl
    {
        private string _myConnection = string.Empty;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender,EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if(IsPostBack) { return; }

            HentOpslag();
        }

        private void HentOpslag()
        {
            var ctrl = new OpslagController();
            var sb = new StringBuilder();

            IEnumerable<OpslagObject> opslagCollection = ctrl.Get(_myConnection, Page.User.Identity.IsAuthenticated, 4);
            foreach(OpslagObject opslag in opslagCollection)
            {
                sb.AppendFormat(@"<b>{0} {1}</b><br/>", DateTimeHelper.DateToStringInGuiFormat(opslag.Dato), opslag.Overskrift);
                if(opslag.Beskrivelse.Length > 100)
                {
                    sb.AppendFormat(@"{0}...", opslag.Beskrivelse.Substring(0,100));
                    sb.AppendFormat(@"<a href='/Nyheder/VisOpslag.aspx#{0}'> læse mere</a>.", opslag.OpslagId);
                }
                else
                {
                    sb.AppendFormat(@"{0}<br/>", opslag.Beskrivelse);
                }
                sb.AppendFormat(@"<hr width='95%'/>");
            }
            spanOpslag.InnerHtml = sb.ToString();
        }
    }
}
