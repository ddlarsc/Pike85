using System;

namespace UserControls
{
    /// <summary>
	///		Summary description for KlubbenUnderMenu.
	/// </summary>
	public partial class KlubbenUnderMenu : System.Web.UI.UserControl
	{

// ReSharper disable InconsistentNaming
		protected void Page_Load(object sender, EventArgs e)
// ReSharper restore InconsistentNaming
		{
			// Put user code to initialize the page here
            if (Context.User.Identity.IsAuthenticated)
            {
                if(Context.User.IsInRole("Administrator"))
                {
                    hlGeneralforsamlingAdmin.Enabled = true;
                    hlGeneralforsamlingAdmin.Visible = true;
                }
                hlGeneralforsamlingView.Visible = true;
                hlGeneralforsamlingView.Enabled = true;
            }
            else
            {
                hlGeneralforsamlingAdmin.Enabled = false;
                hlGeneralforsamlingAdmin.Visible = false;
                hlGeneralforsamlingView.Visible = false;
                hlGeneralforsamlingView.Enabled = false;
            }
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
		{}
		#endregion
	}
}
