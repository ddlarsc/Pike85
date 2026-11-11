namespace Pike85.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
  using System.Web.Security;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for MedlemHovedMenu.
	/// </summary>
	public partial class MedlemHovedMenu : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}
    
    protected void LinkButton_Logud_Click(object sender, System.EventArgs e)
    {
      FormsAuthentication.SignOut();
      Response.Redirect("/Default.aspx");
    }

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

    }
		#endregion
	}
}
