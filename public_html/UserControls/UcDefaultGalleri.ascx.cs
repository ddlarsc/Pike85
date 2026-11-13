using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;

namespace UserControls
{
    public partial class UcDefaultGalleri : System.Web.UI.UserControl
    {
        private const int AntalBilleder = 20;

        protected void Page_Load(object sender, EventArgs e)
        {
            var html = new StringBuilder();
            var found = 0;

            var where = Page.User.Identity.IsAuthenticated ? string.Empty : " AND Offentlig='1'";
            var sql = "SELECT Art, BilledMappe, Billed1 FROM fangster WHERE Billed1 <> ''" + where + " ORDER BY RAND() LIMIT 100";

            using (var connection = new MySqlConnection(Application["MySQLConnString"].ToString()))
            {
                connection.Open();
                using (var command = new MySqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read() && found < AntalBilleder)
                    {
                        var folder = reader["BilledMappe"].ToString();
                        var name = reader["Billed1"].ToString();
                        var thumb = folder + name + "_thumb.jpg";

                        if (!File.Exists(Server.MapPath(thumb)))
                        {
                            continue;
                        }

                        html.Append("<a href=\"" + folder + name + ".jpg\" target=\"_blank\">");
                        html.Append("<img class=\"galleriBilled\" alt=\"" + Server.HtmlEncode(reader["Art"].ToString()) + "\" src=\"" + thumb + "\" />");
                        html.Append("</a>");
                        found++;
                    }
                }
            }

            LiteralGalleri.Text = "<div class=\"galleri\">" + html + "</div>";
        }
    }
}
