using System;
using Pike85;

namespace UserControls
{
    public partial class UcDefaultFangster:System.Web.UI.UserControl
    {
        private string _myConnection = string.Empty;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender,EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if(IsPostBack) { return; }

            HentFangster();
        }

        private void HentFangster()
        {
            string commandText;
            if(Page.User.Identity.IsAuthenticated)
            {
                commandText = "SELECT FangstId, Art, Dato, Vaegt, Fiskevand, BilledMappe, Billed1 FROM fangster ORDER BY Dato DESC LIMIT 0, 20";
            }
            else
            {
                commandText = "SELECT FangstId, Art, Dato, Vaegt, Fiskevand, BilledMappe, Billed1, Offentlig FROM fangster WHERE Offentlig='1' ORDER BY Dato DESC LIMIT 0, 20";
            }
            InternalHelper.DataGridReader(_myConnection,commandText,DataGrid_Fangster);
        }
    }
}
