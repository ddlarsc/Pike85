using System;
using System.Configuration;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

/// <summary>
/// Summary description for Global.
/// </summary>
public class Global : HttpApplication
{
    public Global()
    {
        InitializeComponent();
    }	
		
    protected void Application_Start(Object sender, EventArgs e)
    {
        Application["DSNString"] = ConfigurationManager.AppSettings["DSNString"];
        Application["MySQLConnString"] = ConfigurationManager.AppSettings["MySQLConnString"];
        Application["Pike85Blog"] = ConfigurationManager.AppSettings["Pike85Blog"];
        Application["GreenIcon"] = ConfigurationManager.AppSettings["GreenIcon"];
        Application["YellowIcon"] = ConfigurationManager.AppSettings["YellowIcon"];
        Application["RedIcon"] = ConfigurationManager.AppSettings["RedIcon"];
        Application["SMTPMailPassword"] = ConfigurationManager.AppSettings["SMTPMailPassword"];
    }
 
    protected void Session_Start(Object sender, EventArgs e)
    {

    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {

    }

    protected void Application_EndRequest(Object sender, EventArgs e)
    {

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        // Extract the forms authentication cookie
        string cookieName = FormsAuthentication.FormsCookieName;
        HttpCookie authCookie = Context.Request.Cookies[cookieName];

        if(null == authCookie)
        {
            // There is no authentication cookie.
            return;
        } 

        FormsAuthenticationTicket authTicket;
        try
        {
            authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        }
        catch(Exception)
        {
            // Log exception details (omitted for simplicity)
            return;
        }

        if (null == authTicket)
        {
            // Cookie failed to decrypt.
            return; 
        } 

        // When the ticket was created, the UserData (Roller) property was assigned a
        // pipe delimited string of role names.
        string[] roles = authTicket.UserData.Split(new[]{'|'});

        // Create an Identity object
        var id = new FormsIdentity( authTicket ); 

        // This principal will flow throughout the request.
        var principal = new GenericPrincipal(id, roles);
        // Attach the new principal object to the current HttpContext object
        Context.User = principal;

    }

    protected void Application_Error(Object sender, EventArgs e)
    {

    }

    protected void Session_End(Object sender, EventArgs e)
    {

    }

    protected void Application_End(Object sender, EventArgs e)
    {

    }
			
    #region Web Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {    
        new System.ComponentModel.Container();
    }
    #endregion
}