using System;
using System.Web.Security;

namespace UserControls
{
    /// <summary>
    ///		Summary description for HovedMenu.
    /// </summary>
    public partial class HovedMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HyperLink_Pike85.Visible)
            {
                spanPike85.InnerHtml = "<br/>";
            }

            if (Hyperlink_Klubben.Visible)
            {
                spanklubben.InnerHtml = "<br/>";
            }

            if (HyperLink_Nyheder.Visible)
            {
                spannyheder.InnerHtml = "<br/>";
            }

            if (HyperLink_Opslagstavle.Visible)
            {
                spanopslagstavle.InnerHtml = "<br/>";
            }

            if (Hyperlink_Fangst.Visible)
            {
                spanfangst.InnerHtml = "<br/>";
            }

            if (Context.User.Identity.IsAuthenticated)
            {
                spanlinks.InnerHtml = "<br/>";
                HyperLink_Medlemmer.Visible = true;
                spanlinks.InnerHtml = "<hr/>";

                string mySqlAdminUrl = System.Configuration.ConfigurationManager.AppSettings["MySQLAdminUrl"];
                if (!string.IsNullOrEmpty(mySqlAdminUrl))
                {
                    HyperLink_MySql.NavigateUrl = mySqlAdminUrl;
                    HyperLink_MySql.Visible = true;
                }
            }
            else
            {
                spanlinks.InnerHtml = string.Empty;
                HyperLink_Medlemmer.Visible = false;
            }    
        }

        protected void LinkButtonLogudClick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();

            if (HyperLink_Medlemmer.Visible)
            {
                spanlinks.InnerHtml = string.Empty;
                HyperLink_Medlemmer.Visible = false;
            }

            Response.Redirect("/Default.aspx");
        }

    }
}
