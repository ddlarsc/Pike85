using System.Net.Mail;
using App_Code;
using App_Code.Model;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Security.Principal;

namespace Control
{
    /// <summary>
    /// Summary description for ArrangementController
    /// </summary>
    public class AdminArrangementController
    {
        public void GetSpecificParticipantForAllSetups(string myConnection, IPrincipal user, DropDownList dropDownList)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" SELECT x.*");
            sb.AppendFormat(" FROM(");
            sb.AppendFormat(" SELECT Header, Id");
            sb.AppendFormat(" FROM {0} a", TableHelper.ArrangementSetup);
            sb.AppendFormat(" WHERE enddate >= '{0}'", DateTimeHelper.DateToStringInDbFormat(DateTime.Now));
            if (!user.IsInRole("Administrator"))
            {
                sb.AppendFormat(" AND signature = '{0}'", user.Identity.Name);
            }
            sb.AppendFormat(" ORDER BY startdate asc");
            sb.AppendFormat(" ) x");
            sb.AppendFormat(" UNION");
            sb.AppendFormat(" SELECT y.*");
            sb.AppendFormat(" FROM(");
            sb.AppendFormat(" SELECT '-' as Header, '-' as Id");
            sb.AppendFormat(" ) y");
            sb.AppendFormat(" UNION");
            sb.AppendFormat(" SELECT z.*");
            sb.AppendFormat(" FROM(");
            sb.AppendFormat(" SELECT Header, Id");
            sb.AppendFormat(" FROM {0} a", TableHelper.ArrangementSetup);
            sb.AppendFormat(" WHERE enddate < '{0}'", DateTimeHelper.DateToStringInDbFormat(DateTime.Now));
            if (!user.IsInRole("Administrator"))
            {
                sb.AppendFormat(" AND signature = '{0}'", user.Identity.Name);
            }
            sb.AppendFormat(" ORDER BY startdate desc");
            sb.AppendFormat(" ) z");

            InternalHelper.DropDownListReader(
                myConnection,
                sb.ToString(),
                dropDownList,
                "Header",
                "ID");
        }

        public ArrangementSetupObject GetAllParticipantsForSpecificSetup(string myConnection, int setupId)
        {
            ArrangementSetupObject result = null;
            try
            {
                var arrangements = new List<ArrangementObject>();
                using (var connection = new MySqlConnection(myConnection))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(" SELECT *");
                    sb.AppendFormat(" FROM {0}", TableHelper.ArrangementDeltagelse);
                    sb.AppendFormat(" WHERE setupID = {0}", setupId);

                    var tmpCommand = new MySqlCommand(sb.ToString(), connection);
                    var reader = tmpCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = ObjectHelper.GetIntegerFromObject(reader["id"]);
                            var name = reader["name"].ToString();
                            var decline = ObjectHelper.GetIntegerFromObject(reader["decline"]);
                            var arrivalDate = ObjectHelper.GetDateFromObject(reader["arrivalDate"]);
                            var arrivalOnDay = reader["arrivalOnDay"].ToString();
                            var departureDate = ObjectHelper.GetDateFromObject(reader["departureDate"]);
                            var departureOnDay = reader["departureOnDay"].ToString();
                            var message = reader["messageAny"].ToString();
                            var boat = reader["messageBoat"].ToString();
                            var bed = reader["messageBed"].ToString();
                            arrangements.Add(new ArrangementObject(id, name, setupId, decline, arrivalDate, arrivalOnDay, departureDate, departureOnDay, message, boat, bed));
                            break;
                        }
                    }
                    reader.Close();

                    sb = new StringBuilder();
                    sb.AppendFormat(" SELECT *");
                    sb.AppendFormat(" FROM {0}", TableHelper.ArrangementSetup);
                    sb.AppendFormat(" WHERE ID = {0}", setupId);

                    tmpCommand = new MySqlCommand(sb.ToString(), connection);
                    reader = tmpCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var user = reader["User"].ToString();
                            var deadline = ObjectHelper.GetDateFromObject(reader["Deadline"]);
                            var end = ObjectHelper.GetDateFromObject(reader["EndDate"]);
                            var header = reader["header"].ToString();
                            var id = setupId;
                            var location = reader["place"].ToString();
                            var remarks = reader["remarks"].ToString();
                            var signature = reader["signature"].ToString();
                            var start = ObjectHelper.GetDateFromObject(reader["StartDate"]);
                            var timeStamp = ObjectHelper.GetDateFromObject(reader["timestamp"]);
                            result = new ArrangementSetupObject(id, user, header, location, remarks, deadline, start, end, arrangements, signature, timeStamp);
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

        public void DeleteSetup(string myConnection, int setupId)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" DELETE");
            sb.AppendFormat(" FROM {0}", TableHelper.ArrangementSetup);
            sb.AppendFormat(" WHERE ID = {0}", setupId);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(myConnection, sb);
        }

        public int SetSepcificSetup(string myConnection, ArrangementSetupObject setup)
        {
            if (setup.Id == 0)
            {
                return InsertSetup(myConnection, setup);
            }
            return UpdateSetup(myConnection, setup);
        }

        public void Send(string myConnection, int setupId, string addressTo, EmailSubjectEnum type)
        {
            var ctrl = new AdminArrangementController();
            ArrangementSetupObject setup = ctrl.GetAllParticipantsForSpecificSetup(myConnection, setupId);
            var addressFrom = EnumHelper.GetDescription(MailAddressFrom.NoReply);
            var html = Body(setup, type != EmailSubjectEnum.DeleteArrangement);

            var mailMessageObject = new MailMessageObject(
                addressFrom,
                addressTo,
                true,
                MailPriority.Normal,
                EnumHelper.GetDescription(type),
                html);

            var ctrlMail = new SysMailController();
            ctrlMail.Send(mailMessageObject);
        }

        #region Private Methods

        private static string Body(ArrangementSetupObject setup, bool includeShortCut)
        {
            var sb = new StringBuilder();
            if (setup != null)
            {
                sb.AppendLine("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
                sb.AppendLine("<html xmlns=http://www.w3.org/1999/xhtml'>");
                sb.AppendLine("<head>");
                sb.AppendLine("    <title>Information on Arrangements</title>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");
                sb.AppendLine("    <table width='600px' style='text-align: left'>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td colspan='3'><br /></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80%; font-style:oblique; background-color:GrayText'>Overskrift:</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80%'>" + setup.Header + "</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80%; font-style:oblique; background-color:GrayText'>Sted:</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80%'>" + setup.Location + "</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80% ;font-style:oblique; background-color:GrayText'>Bemærkning:</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80%'>" + setup.Remarks.Replace("\r\n", "<br>") + "</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80% ;font-style:oblique; background-color:GrayText'>Deadline:</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80%'>" + DateTimeHelper.DateToStringInGuiFormat(setup.Deadline) + "</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80% ;font-style:oblique; background-color:GrayText'>Start:</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80%'>" + DateTimeHelper.DateToStringInGuiFormat(setup.Start) + "</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80% ;font-style:oblique; background-color:GrayText'>Slut:</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("            <td style='width:80%'>" + DateTimeHelper.DateToStringInGuiFormat(setup.End) + "</td>");
                sb.AppendLine("            <td style='width:10%'></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("        <tr>");
                sb.AppendLine("            <td colspan='3'><br /></td>");
                sb.AppendLine("        </tr>");
                sb.AppendLine("    </table>");
                if (includeShortCut)
                {
                    sb.AppendLine(" Se hele arrangementer <a");
                    sb.AppendLine(" href='http://pike85.dk/Medlemmer/Arrangement/View.aspx?id=" + setup.Id + "'");
                    sb.AppendLine(" >her</ a>");
                }
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");
            }
            return sb.ToString();
        }

        private static int InsertSetup(string myConnection, ArrangementSetupObject setup)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" INSERT INTO {0} (header, place, remarks, deadline, startdate, enddate, signature, timestamp) VALUES (", TableHelper.ArrangementSetup);
            sb.AppendFormat(" '{0}',", setup.Header);
            sb.AppendFormat(" '{0}',", setup.Location);
            sb.AppendFormat(" '{0}',", setup.Remarks);
            sb.AppendFormat(" '{0}',", DateTimeHelper.DateToStringInDbFormat(setup.Deadline));
            sb.AppendFormat(" '{0}',", DateTimeHelper.DateToStringInDbFormat(setup.Start));
            sb.AppendFormat(" '{0}',", DateTimeHelper.DateToStringInDbFormat(setup.End));
            sb.AppendFormat(" '{0}',", setup.Signature);
            sb.AppendFormat(" '{0}')", DateTimeHelper.DateToStringInDbFormat(setup.TimeStamp));

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(myConnection, sb);

            sb = new StringBuilder();
            sb.AppendFormat(" SELECT MAX(id)");
            sb.AppendFormat(" FROM {0}", TableHelper.ArrangementSetup);

            var result = ctrl.ExecuteScalar(myConnection, sb.ToString());
            return ObjectHelper.GetIntegerFromObject(result);
        }

        private static int UpdateSetup(string myConnection, ArrangementSetupObject setup)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" UPDATE {0} SET", TableHelper.ArrangementSetup);
            sb.AppendFormat(" header = '{0}',", setup.Header);
            sb.AppendFormat(" place = '{0}',", setup.Location);
            sb.AppendFormat(" remarks = '{0}',", setup.Remarks);
            sb.AppendFormat(" deadline = '{0}',", DateTimeHelper.DateToStringInDbFormat(setup.Deadline));
            sb.AppendFormat(" startdate = '{0}',", DateTimeHelper.DateToStringInDbFormat(setup.Start));
            sb.AppendFormat(" enddate = '{0}',", DateTimeHelper.DateToStringInDbFormat(setup.End));
            sb.AppendFormat(" signature = '{0}',", setup.Signature);
            sb.AppendFormat(" timestamp = '{0}'", DateTimeHelper.DateToStringInDbFormat(setup.TimeStamp));
            sb.AppendFormat(" WHERE ID = {0}", setup.Id);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(myConnection, sb);
            return setup.Id;
        }

        #endregion

    }
}