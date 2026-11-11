using System;
using App_Code.Model;

namespace UserControls
{
    public partial class UcGeneralforsamling : System.Web.UI.UserControl
    {
        public GeneralforsamlingObject Generalforsamling
        {
            get;
            set;
        }
// ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
// ReSharper restore InconsistentNaming
        {
            lblBody.Text = Generalforsamling.Body;
            lblHeader.Text = Generalforsamling.Header;
            hlLink.NavigateUrl = Resources.Pike85.GeneralforsamlingURL + Generalforsamling.Link;
            hlLink.Text = Generalforsamling.LinkText;
            lblFooter.Text = Generalforsamling.Footer;
            lblTimestamp.Text = Generalforsamling.Timestamp.ToShortDateString();
        }
    }
}
