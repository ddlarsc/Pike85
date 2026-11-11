using System;
using System.Web.UI;
using App_Code.Model;

namespace UserControls
{
    public partial class MedlemUnderMenu : UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated && Context.User.IsInRole(RollerEnum.Administrator.ToString()))
            {
                HyperLink_RedigerMedlem.Visible = true;
            }
            else
            {
                HyperLink_RedigerMedlem.Visible = false;
            }
        }
    }
}
