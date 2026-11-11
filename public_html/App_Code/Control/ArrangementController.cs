using System.Security.Principal;
using App_Code;
using App_Code.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Model;

namespace Control
{
    /// <summary>
    /// Summary description for ArrangementController
    /// </summary>
    public class ArrangementController
    {
        public List<ArrangementSetupObject> GetSpecificParticipantForCalendar(string myConnection, string user)
        {
            var result = new List<ArrangementSetupObject>();
            try
            {
                using (var connection = new MySqlConnection(myConnection))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(" SELECT ID");
                    sb.AppendFormat(" FROM {0}", TableHelper.ArrangementSetup);
                    sb.AppendFormat(" WHERE");
                    sb.AppendFormat(" (YEAR(STARTDATE) = YEAR(NOW()) AND MONTH(STARTDATE) BETWEEN MONTH(NOW()) AND MONTH(NOW()) + 2)");
                    sb.AppendFormat(" OR");
                    sb.AppendFormat(" (YEAR(ENDDATE) = YEAR(NOW()) AND MONTH(ENDDATE) BETWEEN MONTH(NOW()) AND MONTH(NOW()) + 2)");
                    var tmpCommand = new MySqlCommand(sb.ToString(), connection);
                    var reader = tmpCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var setupId = ObjectHelper.GetIntegerFromObject(reader["ID"]);
                            ArrangementSetupObject setup = GetSpecificParticipantForSpecificSetup(myConnection, setupId, user);
                            result.Add(setup);
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

        public ArrangementSetupObject GetSpecificParticipantForSpecificSetup(string myConnection, int setupId, string name)
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
                    sb.AppendFormat(" AND name = '{0}'", name);

                    var tmpCommand = new MySqlCommand(sb.ToString(), connection);
                    var reader = tmpCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = ObjectHelper.GetIntegerFromObject(reader["id"]);
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

        private ArrangementSetupObject GetAllParticipantsForSpecificSetup(string myConnection, int setupId)
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

        public void GetAllParticipantsForSpecificSetup(string myConnection, DropDownList dropDownList)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" SELECT *");
            sb.AppendFormat(" FROM (");
            sb.AppendFormat(" SELECT header, id ");
            sb.AppendFormat(" FROM {0}", TableHelper.ArrangementSetup);
            sb.AppendFormat(" WHERE enddate >= '{0}'", DateTimeHelper.DateToStringInDbFormat(DateTime.Now));
            sb.AppendFormat(" ORDER BY startdate ASC");
            sb.AppendFormat(" )a");
            sb.AppendFormat(" UNION");
            sb.AppendFormat(" SELECT '----------' as header, 0 as id");
            sb.AppendFormat(" UNION");
            sb.AppendFormat(" SELECT *");
            sb.AppendFormat(" FROM (");
            sb.AppendFormat(" SELECT header, id ");
            sb.AppendFormat(" FROM arrangement_setup");
            sb.AppendFormat(" WHERE enddate < '{0}'", DateTimeHelper.DateToStringInDbFormat(DateTime.Now));
            sb.AppendFormat(" ORDER BY startdate DESC");
            sb.AppendFormat(" )b");

            InternalHelper.DropDownListReader(
                myConnection,
                sb.ToString(),
                dropDownList,
                "Header",
                "id");
        }

        public void GetAllParticipantsForSpecificSetup(string myConnection, DropDownList dropDownList, DataGrid dataGrid)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" SELECT *");
            sb.AppendFormat(" FROM {0}", TableHelper.ArrangementDeltagelse);
            sb.AppendFormat(" WHERE setupID = {0}", dropDownList.SelectedValue);
            sb.AppendFormat(" ORDER BY name ASC");

            InternalHelper.DataGridReader(
                myConnection,
                sb.ToString(),
                dataGrid);
        }

        public void GetAllSetups(string myConnection, DropDownList dropDownList)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" SELECT HEADER, ID");
            sb.AppendFormat(" FROM {0}", TableHelper.ArrangementSetup);
            sb.AppendFormat(" WHERE STARTDATE >= '{0}'", DateTimeHelper.DateToStringInDbFormat(DateTime.Now));
            sb.AppendFormat(" ORDER BY STARTDATE ASC");

            InternalHelper.DropDownListReader(
                myConnection,
                sb.ToString(),
                dropDownList,
                "HEADER",
                "ID");
        }

        public void GetAllSetups(string myConnection, IPrincipal user, DropDownList dropDownList)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" SELECT HEADER, ID");
            sb.AppendFormat(" FROM {0}", TableHelper.ArrangementSetup);
            sb.AppendFormat(" WHERE STARTDATE >= '{0}'", DateTimeHelper.DateToStringInDbFormat(DateTime.Now));
            if (!user.IsInRole(RollerEnum.Administrator.ToString()))
            {
                sb.AppendFormat(@" AND UPPER(USER) = {0}", InternalHelper.ValidateTextForMySql(user.Identity.Name).ToUpper());
            }
            sb.AppendFormat(" ORDER BY STARTDATE ASC");

            InternalHelper.DropDownListReader(
                myConnection,
                sb.ToString(),
                dropDownList,
                "HEADER",
                "ID");
        }

        public void SetSpecificParticipant(string myConnection, ArrangementObject participant)
        {
            if (participant.Id == 0)
            {
                InsertParticipant(myConnection, participant);
            }
            else
            {
                UpdateParticipant(myConnection, participant);
            }
        }

        public string Info(string myConnection, int setupId)
        {
            ArrangementSetupObject setup = GetAllParticipantsForSpecificSetup(myConnection, setupId);
            return Body(setup, false);
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

        private static void InsertParticipant(string myConnection, ArrangementObject arrangementObject)
        {
            var sb = new StringBuilder();

            sb.AppendFormat(" INSERT INTO {0} (name, setupID, decline, arrivalDate, arrivalOnDay, departureDate, departureOnDay, messageAny, messageBoat, messageBed) VALUES (", TableHelper.ArrangementDeltagelse);
            sb.AppendFormat(" '{0}',", arrangementObject.Name);
            sb.AppendFormat(" '{0}',", arrangementObject.SetupId);
            sb.AppendFormat(" '{0}',", arrangementObject.Decline);
            sb.AppendFormat(" '{0}',", DateTimeHelper.DateToStringInDbFormat(arrangementObject.ArrivelDate));
            sb.AppendFormat(" '{0}',", arrangementObject.ArrivalOnDay);
            sb.AppendFormat(" '{0}',", DateTimeHelper.DateToStringInDbFormat(arrangementObject.DepartureDate));
            sb.AppendFormat(" '{0}',", arrangementObject.DepartureOnDay);
            sb.AppendFormat(" '{0}',", arrangementObject.Message);
            sb.AppendFormat(" '{0}',", arrangementObject.MessageAboutBoat);
            sb.AppendFormat(" '{0}')", arrangementObject.MessageAboutBed);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(myConnection, sb);
        }

        private static void UpdateParticipant(string myConnection, ArrangementObject arrangementObject)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" UPDATE {0} SET", TableHelper.ArrangementDeltagelse);
            sb.AppendFormat(" decline = {0},", arrangementObject.Decline);
            sb.AppendFormat(" arrivalDate = '{0}',", DateTimeHelper.DateToStringInDbFormat(arrangementObject.ArrivelDate));
            sb.AppendFormat(" arrivalOnDay = '{0}',", arrangementObject.ArrivalOnDay);
            sb.AppendFormat(" departureDate = '{0}',", DateTimeHelper.DateToStringInDbFormat(arrangementObject.DepartureDate));
            sb.AppendFormat(" departureOnDay = '{0}',", arrangementObject.DepartureOnDay);
            sb.AppendFormat(" messageAny = '{0}',", arrangementObject.Message);
            sb.AppendFormat(" messageBoat = '{0}',", arrangementObject.MessageAboutBoat);
            sb.AppendFormat(" messageBed = '{0}'", arrangementObject.MessageAboutBed);
            sb.AppendFormat(" WHERE ID = {0}", arrangementObject.Id);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(myConnection, sb);
        }

        #endregion
    }
}