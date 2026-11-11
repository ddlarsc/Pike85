using System;
using System.Drawing;

namespace Links
{
    /// <summary>
    /// Summary description for links til forum
    /// </summary>
    public partial class ForumLinks : SkabelonPage
    {
        private string _commandText = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Label_Output.Text = String.Empty;
            if (!Page.IsPostBack)
            {
                _commandText = "SELECT Navn, Adresse, Beskrivelse, Land FROM links WHERE Kategori='Forum' ORDER BY Navn";
                if (!InternalHelper.DataGridReader(Application["MySQLConnString"].ToString(), _commandText, DataGrid_Forumsider))
                {
                    Label_Output.ForeColor = Color.Red;
                    Label_Output.Text = "Links kunne ikke hentes, kontakt webmasteren";
                }
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}
