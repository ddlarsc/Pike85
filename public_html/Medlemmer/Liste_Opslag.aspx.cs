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
using MySql.Data.MySqlClient;

namespace Pike85.Medlemmer
{
    /// <summary>
    /// Summary description for Liste over Opslag.
    /// </summary>
    public partial class Liste_Opslag : SkabelonPage
    {
        private string commandText = string.Empty;

        protected MySqlCommand command;
        protected MySqlConnection connection;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsStartupScriptRegistered("Focus"))
            {
                InternalHelper.SetFocus(DropDownList_Aar);
            }

            if (!Page.IsPostBack)
            {
                Label_Output.Text = string.Empty;

                // Hent årstal fra opslag og indsæt i dropdownliste
                commandText = "SELECT distinct YEAR(Dato) as Dato from opslag order by Dato";
                if (!InternalHelper.DropDownListReader(Application["MySQLConnString"].ToString(), commandText, DropDownList_Aar, "Dato"))
                {
                    Label_Output.ForeColor = Color.Red;
                    Label_Output.Text = "Årstal kunne ikke hentes, kontakt webmasteren";
                }

                // Sæt årstal i dropdownliste til seneste år fra opslag
                DropDownList_Aar.SelectedIndex = DropDownList_Aar.Items.Count - 1;
                DropDownList_Aar_SelectedIndexChanged(this, e);
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
            this.DataGrid_Opslag.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid_Opslag_PageIndexChanged);
            this.DataGrid_Opslag.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid_Opslag_EditCommand);
            this.DataGrid_Opslag.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid_Opslag_DeleteCommand);

        }
        #endregion

        private void DataGrid_Opslag_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                connection.Open();
                command = new MySqlCommand("DELETE FROM opslag WHERE OpslagId = '" + e.Item.Cells[0].Text + "'", connection);
                command.ExecuteNonQuery();

                DataGrid_Opslag.CurrentPageIndex = 0;
                if (!BindData())
                {
                    Label_Output.ForeColor = Color.Red;
                    Label_Output.Text = "Opslag kunne ikke hentes, kontakt webmasteren";
                }
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void DataGrid_Opslag_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            Response.Redirect("/Medlemmer/Rediger_Opslag.aspx?OpslagId=" + e.Item.Cells[0].Text);
        }

        private void DataGrid_Fangster_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            DataGrid_Opslag.CurrentPageIndex = e.NewPageIndex;

            if (!BindData())
            {
                Label_Output.ForeColor = Color.Red;
                Label_Output.Text = "Opslag kunne ikke hentes, kontakt webmasteren";
            }
        }

        private bool BindData()
        {
            commandText = "SELECT OpslagId, Dato, Overskrift, Beskrivelse FROM opslag WHERE YEAR(Dato)='" + DropDownList_Aar.SelectedItem.Value + "'ORDER BY Dato DESC";

            bool success = false;
            try
            {
                connection = new MySqlConnection(Application["MySQLConnString"].ToString());

                connection.Open();
                command = new MySqlCommand(commandText, connection);
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(commandText, connection);
                DataSet objDataSet = new DataSet();
                objAdapter.Fill(objDataSet);
                DataGrid_Opslag.DataSource = objDataSet;
                DataGrid_Opslag.DataBind();
                success = true;
            }
            catch
            {
                throw;
                //success = false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return success;
        }

        protected void DropDownList_Aar_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!BindData())
            {
                Label_Output.ForeColor = Color.Red;
                Label_Output.Text = "Opslag kunne ikke hentes, kontakt webmasteren";
            }

        }

        private void DataGrid_Opslag_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            DataGrid_Opslag.CurrentPageIndex = e.NewPageIndex;

            if (!BindData())
            {
                Label_Output.ForeColor = Color.Red;
                Label_Output.Text = "Opslag kunne ikke hentes, kontakt webmasteren";
            }
        }

    }
}
