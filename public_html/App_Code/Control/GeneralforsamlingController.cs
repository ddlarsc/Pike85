using System;
using System.Collections.Generic;
using App_Code;
using App_Code.Model;
using MySql.Data.MySqlClient;

namespace Control
{
    /// <summary>
    /// Summary description for AdminGeneralforsamlingController
    /// </summary>
    public class GeneralforsamlingController : SkabelonPage
    {
        public IEnumerable<GeneralforsamlingObject> SelectTop5(string conn)
        {
            var result = new List<GeneralforsamlingObject>();
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    string arg = string.Format("SELECT * FROM {0} ORDER BY PK DESC LIMIT 5", TableHelper.Generalforsamling);
                    var command = new MySqlCommand(arg, connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(new GeneralforsamlingObject
                                         {
                                             Body = reader.GetString("BODY"),
                                             Footer = reader.GetString("FOOTER"),
                                             Header = reader.GetString("HEADER"),
                                             Link = reader.GetString("LINK"),
                                             LinkText = reader.GetString("LINKTEXT"),
                                             Pk = reader.GetInt32("PK"),
                                             Signature = reader.GetString("SIGNATURE"),
                                             Timestamp = reader.GetDateTime("TIMESTAMP")
                                         });
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
    }
}
