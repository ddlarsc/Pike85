using System;
using System.Text;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;
using MySql.Data.MySqlClient;

namespace Control
{
    /// <summary>
    /// Summary description for GeneralforsamlingController
    /// </summary>
    public class AdminGeneralforsamlingController : SkabelonPage
    {
        public void SelectAll(string conn, DropDownList list, string field, string value)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (string.IsNullOrEmpty(field))
            {
                throw new ArgumentNullException("field");
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }
            if (!InternalHelper.DropDownListReader(conn, string.Format("SELECT * FROM {0} ORDER BY PK DESC", TableHelper.Generalforsamling), list, field, value))
            {
                //throw;
            }
        }

        public GeneralforsamlingObject Select(string conn, int pk)
        {
            GeneralforsamlingObject result = null;
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var command = new MySqlCommand(string.Format("SELECT * FROM {0} WHERE PK = {1}", TableHelper.Generalforsamling, pk), connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = new GeneralforsamlingObject
                                         {
                                             Body = reader.GetString("body"),
                                             Footer = reader.GetString("footer"),
                                             Header = reader.GetString("header"),
                                             Link = reader.GetString("link"),
                                             LinkText = reader.GetString("linkText"),
                                             Pk = pk,
                                             Signature = reader.GetString("signature"),
                                             Timestamp = reader.GetDateTime("Timestamp")
                                         };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
            }
            return result;
        }

        public void Insert(string conn, GeneralforsamlingObject generalforsamlingObject)
        {
            var commandText = new StringBuilder();
            commandText.AppendFormat(" Insert into {0} (Header, Body, Link, LinkText, Footer, Signature, Timestamp)", TableHelper.Generalforsamling);
            commandText.AppendFormat(" Values (");
            commandText.AppendFormat(" '{0}',", generalforsamlingObject.Header);
            commandText.AppendFormat(" '{0}',", generalforsamlingObject.Body);
            commandText.AppendFormat(" '{0}',", generalforsamlingObject.Link);
            commandText.AppendFormat(" '{0}',", generalforsamlingObject.LinkText);
            commandText.AppendFormat(" '{0}',", generalforsamlingObject.Footer);
            commandText.AppendFormat(" '{0}',", generalforsamlingObject.Signature);
            commandText.AppendFormat(" '{0}')", generalforsamlingObject.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(conn, commandText);
        }

        public void Update(string conn, GeneralforsamlingObject generalforsamlingObject)
        {
            var commandText = new StringBuilder();
            commandText.AppendFormat(" Update {0} Set", TableHelper.Generalforsamling);
            commandText.AppendFormat(" Header = '{0}',", generalforsamlingObject.Header);
            commandText.AppendFormat(" Body = '{0}',", generalforsamlingObject.Body);
            if (generalforsamlingObject.Link != string.Empty)
            {
                commandText.AppendFormat(" Link = '{0}',", generalforsamlingObject.Link);
            }
            commandText.AppendFormat(" LinkText = '{0}',", generalforsamlingObject.LinkText);
            commandText.AppendFormat(" Footer = '{0}',", generalforsamlingObject.Footer);
            commandText.AppendFormat(" Signature = '{0}'", generalforsamlingObject.Signature);
            commandText.AppendFormat(" Where Pk = {0}", generalforsamlingObject.Pk);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(conn, commandText);
        }

        public void Delete(string conn, int pk)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" DELETE");
            sb.AppendFormat(" FROM {0}", TableHelper.Generalforsamling);
            sb.AppendFormat(" WHERE PK = {0}", pk);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(conn, sb);
        }
    }
}
