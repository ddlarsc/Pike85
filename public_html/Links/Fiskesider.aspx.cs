using System;
using System.Drawing;

namespace Links
{
    /// <summary>
    /// Summary description for Fiskesider links.
    /// </summary>
    public partial class FiskeLinks : SkabelonPage
    {
        private string _commandText = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownListLandSelectedIndexChanged();
            if (!Page.IsPostBack)
            {
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

        private void DropDownListLandSelectedIndexChanged()
        {
            Label_Output.Text = String.Empty;

            _commandText = "SELECT Navn, Adresse, Beskrivelse, Land FROM links WHERE Land='" +
              DropDownList_Land.SelectedItem.Value + "' AND Kategori='Fiskeri' ORDER BY Navn";
            if (!InternalHelper.DataGridReader(Application["MySQLConnString"].ToString(), _commandText, DataGrid_Fiskesider))
            {
                Label_Output.ForeColor = Color.Red;
                Label_Output.Text = "Links kunne ikke hentes, kontakt webmasteren";
            }

        }
    }
}
