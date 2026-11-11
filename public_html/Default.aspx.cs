using System;
using System.Web.UI;
using UserControls;

public partial class Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HentPokaler();
        HentFangster();
        HentBlogger();
        HentNyheder();
        HentOpslag();
        //HentBlogOversigt(); //ændring pr. 170715
    }

    #region Private Helpers

    private void HentPokaler()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            var userControl = (UcDefaultPokaler)Page.LoadControl("~/UserControls/UcDefaultPokaler.ascx");
            phPokalOversigt.Controls.Add(userControl);
        }
    }

    private void HentFangster()
    {
        var userControl = (UcDefaultFangster)Page.LoadControl("~/UserControls/UcDefaultFangster.ascx");
        phFangster.Controls.Add(userControl);
    }

    private void HentBlogger()
    {
        var userControl = (UcBlog)Page.LoadControl("~/UserControls/UcBlog.ascx");
        phBlogger.Controls.Add(userControl);
    }

    private void HentNyheder()
    {
        var userControl = (UcDefaultNyheder)Page.LoadControl("~/UserControls/UcDefaultNyheder.ascx");
        phNyheder.Controls.Add(userControl);
    }

    private void HentOpslag()
    {
        var userControl = (UcDefaultOpslag)Page.LoadControl("~/UserControls/UcDefaultOpslag.ascx");
        phOpslag.Controls.Add(userControl);
    }

    //ændring pr. 170715
    //private void HentBlogOversigt()
    //{
    //    var userControl = Page.LoadControl("~/UserControls/UcBlogOversigt.ascx");
    //    phBlogOversigt.Controls.Add(userControl);
    //}

    #endregion
}
