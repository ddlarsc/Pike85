using System;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;

namespace Medlemmer
{
    /// <summary>
    /// Summary description for Liste over Fangstrapporter.
    /// </summary>
    public partial class ListeFangstrapporter : SkabelonPage
    {
        private string _commandText = string.Empty;

        private MySqlCommand _command;
        private MySqlConnection _connection;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsStartupScriptRegistered("Focus"))
            {
                InternalHelper.SetFocus(DropDownList_Aar);
            }

            if (!Page.IsPostBack)
            {
                // Hent årstal fra fangsterne og indsæt i dropdownliste
                _commandText = "SELECT distinct YEAR(Dato) as Dato from fangster order by Dato";
                if (!InternalHelper.DropDownListReader(Application["MySQLConnString"].ToString(), _commandText, DropDownList_Aar, "Dato"))
                {
                    Label_Output.ForeColor = Color.Red;
                    Label_Output.Text = "Årstal kunne ikke hentes, kontakt webmasteren";
                }

                // Sæt årstal i dropdownliste til seneste år fra fangsterne
                DropDownList_Aar.SelectedIndex = DropDownList_Aar.Items.Count - 1;
                DropDownListAarSelectedIndexChanged(this, e);
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
            this.DataGrid_Fangster.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridFangsterPageIndexChanged);
            this.DataGrid_Fangster.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridFangsterEditCommand);
            this.DataGrid_Fangster.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridFangsterDeleteCommand);

        }
        #endregion

        private void DataGridFangsterDeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                _connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                _connection.Open();
                _command = new MySqlCommand("DELETE FROM fangster WHERE FangstId = '" + e.Item.Cells[0].Text + "'", _connection);
                _command.ExecuteNonQuery();
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            DataGrid_Fangster.CurrentPageIndex = 0;
            if (!BindData())
            {
                Label_Output.ForeColor = Color.Red;
                Label_Output.Text = "Fangsterne kunne ikke hentes, kontakt webmasteren";
            }
        }

        private void DataGridFangsterEditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Response.Redirect("/Medlemmer/Rediger_Fangstrapport.aspx?FangstId=" + e.Item.Cells[0].Text);
        }

        private void DataGridFangsterPageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            DataGrid_Fangster.CurrentPageIndex = e.NewPageIndex;

            if (!BindData())
            {
                Label_Output.ForeColor = Color.Red;
                Label_Output.Text = "Fangsterne kunne ikke hentes, kontakt webmasteren";
            }
        }

        private bool BindData()
        {
            _commandText = "SELECT FangstId, Dato, Art, Vaegt, Fanger, Fiskevand, Land FROM fangster WHERE YEAR(Dato)='" + DropDownList_Aar.SelectedItem.Value + "'ORDER BY Dato DESC";

            bool success;
            try
            {
                _connection = new MySqlConnection(Application["MySQLConnString"].ToString());

                _connection.Open();
                _command = new MySqlCommand(_commandText, _connection);

                var objAdapter = new MySqlDataAdapter(_commandText, _connection);
                var objDataSet = new DataSet();
                objAdapter.Fill(objDataSet);
                DataGrid_Fangster.DataSource = objDataSet;
                DataGrid_Fangster.DataBind();
                success = true;
            }
            catch
            {
                success = false;
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return success;
        }

        protected void DropDownListAarSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!BindData())
            {
                Label_Output.ForeColor = Color.Red;
                Label_Output.Text = "Fangsterne kunne ikke hentes, kontakt webmasteren";
            }
        }

    }
}
