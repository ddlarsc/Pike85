using System;
using Control;

namespace UserControls
{
    public partial class UcDefaultPokaler:System.Web.UI.UserControl
    {
        private string _myConnection = string.Empty;

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender,EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            _myConnection = Application["MySQLConnString"].ToString();

            if(IsPostBack) { return; }

            var ctrl = new FangsterController();
            InternalHelper.DataGridReader(_myConnection, ctrl.GetTrohpies(DateTime.Now.Year), DataGrid_Pokaler);
        }
    }
}
