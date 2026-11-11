using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Pike85.Medlemmer
{
    /// <summary>
    /// Summary description for HentBilled.
    /// </summary>
    public partial class HentBilled : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            FileStream file = new FileStream(Server.MapPath("/Billeder/Nyheder/") + Request.QueryString["FilNavn"], FileMode.Open);
            Response.Write(Request.QueryString["FilNavn"]);
            Response.End();
            byte[] b = new byte[file.Length];
            file.Read(b, 0, (int)file.Length);
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(b);
            Response.End();
            file.Close();
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}
