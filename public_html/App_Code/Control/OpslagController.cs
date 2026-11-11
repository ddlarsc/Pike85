using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;
using MySql.Data.MySqlClient;

namespace Control
{
    public class OpslagController
    {
        private const string TableNameOpslag = "opslag";

        public OpslagObject Get(string conn, int id)
        {
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(@" SELECT *");
                    sb.AppendFormat(@" FROM {0}", TableNameOpslag);
                    sb.AppendFormat(@" WHERE OPSLAGID = {0}", id);

                    var command = new MySqlCommand(sb.ToString(), connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return new OpslagObject
                                               {
                                                   Beskrivelse = ObjectHelper.GetStringFormObject(reader["BESKRIVELSE"]),
                                                   Billed = ObjectHelper.GetStringFormObject(reader["BILLED"]),
                                                   BilledMappe = ObjectHelper.GetStringFormObject(reader["BILLEDMAPPE"]),
                                                   Dato = ObjectHelper.GetDateFromObject(reader["DATO"]),
                                                   Forfatter = ObjectHelper.GetStringFormObject(reader["FORFATTER"]),
                                                   Link = ObjectHelper.GetStringFormObject(reader["LINK"]),
                                                   Offentlig = ObjectHelper.GetBooleanFromObject(reader["OFFENTLIG"]),
                                                   OpslagId = id,
                                                   Overskrift = ObjectHelper.GetStringFormObject(reader["OVERSKRIFT"]),
                                                   Video1 = ObjectHelper.GetStringFormObject(reader["VIDEO1"]),
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
            return null;
        }

        public IEnumerable<OpslagObject> Get(string conn, bool isAuthenticated, int entries)
        {
            var result = new List<OpslagObject>();
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(@" SELECT *");
                    sb.AppendFormat(@" FROM {0}", TableNameOpslag);
                    if (!isAuthenticated)
                    {
                        sb.AppendFormat(@" WHERE Offentlig='1'");
                    }
                    sb.AppendFormat(@" ORDER BY DATO");
                    sb.AppendFormat(@" DESC LIMIT 0, {0}", entries);

                    var command = new MySqlCommand(sb.ToString(), connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(
                                new OpslagObject
                                    {
                                        Beskrivelse = ObjectHelper.GetStringFormObject(reader["BESKRIVELSE"]),
                                        Billed = ObjectHelper.GetStringFormObject(reader["BILLED"]),
                                        BilledMappe = ObjectHelper.GetStringFormObject(reader["BILLEDMAPPE"]),
                                        Dato = ObjectHelper.GetDateFromObject(reader["DATO"]),
                                        Forfatter = ObjectHelper.GetStringFormObject(reader["FORFATTER"]),
                                        Link = ObjectHelper.GetStringFormObject(reader["LINK"]),
                                        Offentlig = ObjectHelper.GetBooleanFromObject(reader["OFFENTLIG"]),
                                        OpslagId = ObjectHelper.GetIntegerFromObject(reader["OPSLAGID"]),
                                        Overskrift = ObjectHelper.GetStringFormObject(reader["OVERSKRIFT"]),
                                        Video1 = ObjectHelper.GetStringFormObject(reader["VIDEO1"]),
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

        public void Get(string connection, IPrincipal user, DropDownList dropdown)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" SELECT OPSLAGID, CONCAT(CONCAT(YEAR(DATO),'  '),OVERSKRIFT) AS OVERSKRIFT");
            sb.AppendFormat(@" FROM {0}", TableNameOpslag);
            if (!user.IsInRole(RollerEnum.Administrator.ToString()))
            {
                sb.AppendFormat(@" WHERE UPPER(FORFATTER) = {0}", InternalHelper.ValidateTextForMySql(user.Identity.Name).ToUpper());
            }
            sb.AppendFormat(@" ORDER BY OPSLAGID DESC");

            InternalHelper.DropDownListReader(
                connection,
                sb.ToString(),
                dropdown,
                @"OVERSKRIFT",
                @"OPSLAGID");
        }

        public void Insert(string connection, OpslagObject opslagObject)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" INSERT INTO {0} (BESKRIVELSE, BILLED, BILLEDMAPPE, DATO, FORFATTER, LINK, OFFENTLIG, OVERSKRIFT, VIDEO1)", TableNameOpslag);
            sb.AppendFormat(@" VALUES (");
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(opslagObject.Beskrivelse));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(opslagObject.Billed));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(opslagObject.BilledMappe));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(opslagObject.Dato)));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(opslagObject.Forfatter));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(opslagObject.Link));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(Convert.ToSByte(opslagObject.Offentlig).ToString(CultureInfo.InvariantCulture)));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(opslagObject.Overskrift));
            sb.AppendFormat(@" {0})", InternalHelper.ValidateTextForMySql(opslagObject.Video1));
            sb.AppendFormat(@" ;");
            sb.AppendFormat(@" SELECT MAX(OPSLAGID) AS ID");
            sb.AppendFormat(@" FROM {0}", TableNameOpslag);
            sb.AppendFormat(@" ;");

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteScalar(connection, sb);
        }

        public void Update(string connection, OpslagObject opslagObject)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" UPDATE {0} SET", TableNameOpslag);
            sb.AppendFormat(@" BESKRIVELSE = {0},", InternalHelper.ValidateTextForMySql(opslagObject.Beskrivelse));
            sb.AppendFormat(@" BILLED = {0},", InternalHelper.ValidateTextForMySql(opslagObject.Billed));
            sb.AppendFormat(@" BILLEDMAPPE = {0},", InternalHelper.ValidateTextForMySql(opslagObject.BilledMappe));
            sb.AppendFormat(@" DATO = {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(opslagObject.Dato)));
            sb.AppendFormat(@" FORFATTER = {0},", InternalHelper.ValidateTextForMySql(opslagObject.Forfatter));
            sb.AppendFormat(@" LINK = {0},", InternalHelper.ValidateTextForMySql(opslagObject.Link));
            sb.AppendFormat(@" OFFENTLIG = {0},", InternalHelper.ValidateTextForMySql(Convert.ToSByte(opslagObject.Offentlig).ToString(CultureInfo.InvariantCulture)));
            sb.AppendFormat(@" OVERSKRIFT = {0},", InternalHelper.ValidateTextForMySql(opslagObject.Overskrift));
            sb.AppendFormat(@" VIDEO1 = {0}", InternalHelper.ValidateTextForMySql(opslagObject.Video1));
            sb.AppendFormat(@" WHERE OpslagId = {0}", InternalHelper.ValidateTextForMySql(opslagObject.OpslagId.ToString(CultureInfo.InvariantCulture)));

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }

        public void Delete(string connection, int id)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" DELETE");
            sb.AppendFormat(@" FROM {0}", TableNameOpslag);
            sb.AppendFormat(@" WHERE OPSLAGID = {0}", id);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }
    }
}