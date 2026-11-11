using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;

namespace Pike85
{
    /// <summary>
    /// Summary description for KlubRekorder.
    /// </summary>
    public partial class Pokaler : SkabelonPage
    {
        protected static MySqlCommand command;
        protected static MySqlConnection connection;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string commandText = string.Empty;
            Label_Output.Visible = false;
            Label_Output.Text = string.Empty;

            if (!Page.IsPostBack)
            {
                try
                {
                    // Create main table for all trophys 
                    DataTable dtTrophy = new DataTable();

                    dtTrophy.Columns.Add("Year");
                    dtTrophy.Columns.Add("Pike");
                    dtTrophy.Columns.Add("Cod");
                    dtTrophy.Columns.Add("Salmon");
                    dtTrophy.Columns.Add("YearFish");
                    dtTrophy.PrimaryKey = new DataColumn[] { dtTrophy.Columns["Year"] };

                    // Create and open connection
                    connection = new MySqlConnection(Application["MySQLConnString"].ToString());
                    connection.Open();

                    // Command for all year Pike records from 1985 to current year
                    command = new MySqlCommand(
                        "SELECT YEAR(f1.Dato) as Dato, f1.Fanger, f1.Art, FORMAT(f1.Vaegt,3) as Vaegt " +
                        "FROM" +
                        "(SELECT YEAR(Dato) as grpDato, max(Vaegt) as maxVaegt " +
                        "FROM fangster WHERE Art='Gedde' AND YEAR(Dato) > 1984 GROUP BY YEAR(Dato)) AS f2 " +
                        "INNER JOIN fangster AS f1 ON YEAR(f1.Dato) = grpDato AND " +
                        "f1.Vaegt = f2.maxVaegt AND Art = 'Gedde' ORDER BY YEAR(Dato) DESC", connection);

                    // Table for biggest pike in the all years
                    DataTable dtPike = new DataTable();
                    dtPike.Load(command.ExecuteReader());

                    // Loop over all pikes and add them to main trophy table.
                    foreach (DataRow drPike in dtPike.Rows)
                    {
                        DataRow drPokaler;
                        if (dtTrophy.Rows.Contains(drPike.ItemArray.GetValue(0).ToString()))
                        {
                            drPokaler = dtTrophy.Rows.Find(drPike.ItemArray.GetValue(0).ToString());
                            if (String.IsNullOrEmpty(drPokaler["Pike"].ToString()))
                            {
                                drPokaler["Pike"] = drPike.ItemArray.GetValue(1) + "<br/>" + drPokaler["Pike"];
                            }
                            else
                            {
                                drPokaler["Pike"] = drPike.ItemArray.GetValue(1) + " og <br/>" + drPokaler["Pike"];
                            }
                        }
                        else
                        {
                            drPokaler = dtTrophy.NewRow();
                            drPokaler["Year"] = drPike.ItemArray.GetValue(0);
                            drPokaler["Pike"] = drPike.ItemArray.GetValue(1) + "<br/>" + drPike.ItemArray.GetValue(3);
                            dtTrophy.Rows.Add(drPokaler);
                        }
                    }

                    dtPike.EndLoadData();
                    dtPike.Dispose();

                    // Command for all year Cod records from 2005 to current year
                    command = new MySqlCommand(
                        "SELECT YEAR(f1.Dato) as Dato, f1.Fanger, f1.Art, FORMAT(f1.Vaegt,3) as Vaegt " +
                        "FROM" +
                        "(SELECT YEAR(Dato) as grpDato, max(Vaegt) as maxVaegt " +
                        "FROM fangster WHERE Art='Torsk' AND YEAR(Dato) > 2004 GROUP BY YEAR(Dato)) AS f2 " +
                        "INNER JOIN fangster AS f1 ON YEAR(f1.Dato) = grpDato AND " +
                        "f1.Vaegt = f2.maxVaegt AND Art = 'Torsk' ORDER BY YEAR(Dato) DESC", connection);

                    // Table for biggest cod in the all years
                    DataTable dtCod = new DataTable();
                    dtCod.Load(command.ExecuteReader());

                    // Loop over all cods and add them to main trophy table.
                    foreach (DataRow drCod in dtCod.Rows)
                    {
                        DataRow drPokaler;
                        if (dtTrophy.Rows.Contains(drCod.ItemArray.GetValue(0).ToString()))
                        {
                            drPokaler = dtTrophy.Rows.Find(drCod.ItemArray.GetValue(0).ToString());
                            if (String.IsNullOrEmpty(drPokaler["Cod"].ToString()))
                            {
                                drPokaler["Cod"] = drCod.ItemArray.GetValue(1) + "<br/>" + drCod.ItemArray.GetValue(3);
                            }
                            else
                            {
                                drPokaler["Cod"] = drCod.ItemArray.GetValue(1) + " og <br/>" + drPokaler["Cod"];
                            }
                        }
                    }

                    dtCod.EndLoadData();
                    dtCod.Dispose();

                    // Command for all year Salmon records from 2000 to current year
                    command = new MySqlCommand(
                        "SELECT YEAR(f1.Dato) as Dato, f1.Fanger, f1.Art, FORMAT(f1.Vaegt,3) as Vaegt " +
                        "FROM" +
                        "(SELECT YEAR(Dato) as grpDato, max(Vaegt) as maxVaegt " +
                        "FROM fangster WHERE Art='Laks' AND YEAR(Dato) > 1999 GROUP BY YEAR(Dato)) AS f2 " +
                        "INNER JOIN fangster AS f1 ON YEAR(f1.Dato) = grpDato AND " +
                        "f1.Vaegt = f2.maxVaegt AND Art = 'Laks' ORDER BY YEAR(Dato) DESC", connection);

                    // Table for biggest salmon in the all years
                    DataTable dtSalmon = new DataTable();
                    dtSalmon.Load(command.ExecuteReader());

                    // Loop over all cods and add them to main trophy table.
                    foreach (DataRow drSalmon in dtSalmon.Rows)
                    {
                        DataRow drPokaler;
                        if (dtTrophy.Rows.Contains(drSalmon.ItemArray.GetValue(0).ToString()))
                        {
                            drPokaler = dtTrophy.Rows.Find(drSalmon.ItemArray.GetValue(0).ToString());
                            if (String.IsNullOrEmpty(drPokaler["Salmon"].ToString()))
                            {
                                drPokaler["Salmon"] = drSalmon.ItemArray.GetValue(1) + "<br/>" + drSalmon.ItemArray.GetValue(3);
                            }
                            else
                            {
                                drPokaler["Salmon"] = drSalmon.ItemArray.GetValue(1) + " og <br/>" + drPokaler["Salmon"];
                            }
                        }
                    }

                    dtSalmon.EndLoadData();
                    dtSalmon.Dispose();

                    // Command for all fish of the year records from 1999 to current year
                    command = new MySqlCommand(
                        "SELECT YEAR(f1.Dato) as Dato, f1.Fanger, f1.Art, FORMAT(f1.Vaegt,3) as Vaegt, f1.YearFish " +
                        "FROM" +
                        "(SELECT YEAR(Dato) as grpDato, max(Vaegt) as maxVaegt " +
                        "FROM fangster WHERE YearFish='1' AND YEAR(Dato) > 1998 GROUP BY YEAR(Dato)) AS f2 " +
                        "INNER JOIN fangster AS f1 ON YEAR(f1.Dato) = grpDato AND " +
                        "f1.Vaegt = f2.maxVaegt AND f1.YearFish = '1' ORDER BY YEAR(Dato) DESC", connection);

                    // Table for biggest  fish of the year in the all years
                    DataTable dtYearFish = new DataTable();
                    dtYearFish.Load(command.ExecuteReader());

                    // Loop over all cods and add them to main trophy table.
                    foreach (DataRow drYearFish in dtYearFish.Rows)
                    {
                        DataRow drPokaler;

                        if (dtTrophy.Rows.Contains(drYearFish.ItemArray.GetValue(0).ToString()))
                        {
                            drPokaler = dtTrophy.Rows.Find(drYearFish.ItemArray.GetValue(0).ToString());
                            if (String.IsNullOrEmpty(drPokaler["YearFish"].ToString()))
                            {
                                drPokaler["YearFish"] = drYearFish.ItemArray.GetValue(1) + "<br/>" + drYearFish.ItemArray.GetValue(2) + "<br/>" + drYearFish.ItemArray.GetValue(3);
                            }
                            else
                            {
                                drPokaler["YearFish"] = drYearFish.ItemArray.GetValue(1) + " og <br/>" + drPokaler["YearFish"];
                            }
                        }
                    }

                    dtYearFish.EndLoadData();
                    dtYearFish.Dispose();                    

                    GridViewPokaler.DataSource = dtTrophy;
                    GridViewPokaler.DataBind();

                    dtTrophy.EndLoadData();
                    dtYearFish.Dispose();
                }
                catch (Exception ex)
                {
                    Label_Output.Visible = true;
                    Label_Output.ForeColor = Color.Red;
                    Label_Output.Text = "Der er sket en fejl da Pokaler skulle vises... " + ex.Message;
                }
                finally 
                { 
                    connection.Close(); 
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
