using System;

namespace UserControls
{
    ///<summary>
    ///Summary description for UcKlubbenUnderMenu.
    ///</summary>
    public partial class UcKlubbenUnderMenu:System.Web.UI.UserControl
    {
        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender,EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if(Context.User.IsInRole("Administrator"))
            {
                hlGeneralforsamlingAdmin.Enabled = true;
                hlGeneralforsamlingAdmin.Visible = true;
            }
            else
            {
                hlGeneralforsamlingAdmin.Enabled = false;
                hlGeneralforsamlingAdmin.Visible = false;
            }
            if(Context.User.Identity.IsAuthenticated)
            {
                hlGeneralforsamlingView.Visible = true;
                hlGeneralforsamlingView.Enabled = true;
            }
            else
            {
                hlGeneralforsamlingView.Visible = false;
                hlGeneralforsamlingView.Enabled = false;
            }
        }
    }
}
