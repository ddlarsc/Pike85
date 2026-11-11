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
    public class NyhedController
    {
        private const string TableNameNyheder = "nyheder";

        public NyhedObject Get(string conn, int id)
        {
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(@" SELECT *");
                    sb.AppendFormat(@" FROM {0}", TableNameNyheder);
                    sb.AppendFormat(@" WHERE ID = {0}", id);

                    var command = new MySqlCommand(sb.ToString(), connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var forfatter = ObjectHelper.GetStringFormObject(reader["FORFATTER"]);
                            var dato = ObjectHelper.GetDateFromObject(reader["DATO"]);
                            var overskrift = ObjectHelper.GetStringFormObject(reader["OVERSKRIFT"]);
                            var beskrivelse = ObjectHelper.GetStringFormObject(reader["BESKRIVELSE"]);
                            var link = ObjectHelper.GetStringFormObject(reader["LINK"]);
                            var billedMappe = ObjectHelper.GetStringFormObject(reader["BILLEDMAPPE"]);
                            var billed1 = ObjectHelper.GetStringFormObject(reader["BILLED1"]);
                            var billed2 = ObjectHelper.GetStringFormObject(reader["BILLED2"]);
                            var billed3 = ObjectHelper.GetStringFormObject(reader["BILLED3"]);
                            var video1 = ObjectHelper.GetStringFormObject(reader["VIDEO1"]);
                            var video2 = ObjectHelper.GetStringFormObject(reader["VIDEO2"]);
                            var video3 = ObjectHelper.GetStringFormObject(reader["VIDEO3"]);
                            var offentlig = ObjectHelper.GetBooleanFromObject(reader["OFFENTLIG"]);
                            return new NyhedObject(id, forfatter, dato, overskrift, beskrivelse, link, billedMappe, billed1, billed2, billed3, video1, video2, video3, offentlig);
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

        public List<NyhedObject> Get(string conn, bool isAuthenticated, int entries)
        {
            var result = new List<NyhedObject>();
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();
                    var sb = new StringBuilder();
                    sb.AppendFormat(@" SELECT *");
                    sb.AppendFormat(@" FROM {0}", TableNameNyheder);
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
                            var id = ObjectHelper.GetIntegerFromObject(reader["ID"]);
                            var forfatter = ObjectHelper.GetStringFormObject(reader["FORFATTER"]);
                            var dato = ObjectHelper.GetDateFromObject(reader["DATO"]);
                            var overskrift = ObjectHelper.GetStringFormObject(reader["OVERSKRIFT"]);
                            var beskrivelse = ObjectHelper.GetStringFormObject(reader["BESKRIVELSE"]);
                            var link = ObjectHelper.GetStringFormObject(reader["LINK"]);
                            var billedMappe = ObjectHelper.GetStringFormObject(reader["BILLEDMAPPE"]);
                            var billed1 = ObjectHelper.GetStringFormObject(reader["BILLED1"]);
                            var billed2 = ObjectHelper.GetStringFormObject(reader["BILLED2"]);
                            var billed3 = ObjectHelper.GetStringFormObject(reader["BILLED3"]);
                            var video1 = ObjectHelper.GetStringFormObject(reader["VIDEO1"]);
                            var video2 = ObjectHelper.GetStringFormObject(reader["VIDEO2"]);
                            var video3 = ObjectHelper.GetStringFormObject(reader["VIDEO3"]);
                            var offentlig = ObjectHelper.GetBooleanFromObject(reader["OFFENTLIG"]);

                            result.Add(new NyhedObject(id, forfatter, dato, overskrift, beskrivelse, link, billedMappe, billed1, billed2, billed3, video1, video2, video3, offentlig));
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
            sb.AppendFormat(@" SELECT ID, CONCAT(CONCAT(YEAR(DATO),'  '),OVERSKRIFT) AS OVERSKRIFT");
            sb.AppendFormat(@" FROM {0}", TableNameNyheder);
            if (!user.IsInRole(RollerEnum.Administrator.ToString()))
            {
                sb.AppendFormat(@" WHERE UPPER(FORFATTER) = {0}", InternalHelper.ValidateTextForMySql(user.Identity.Name).ToUpper());
            }
            sb.AppendFormat(@" ORDER BY ID DESC");

            InternalHelper.DropDownListReader(
                connection,
                sb.ToString(),
                dropdown,
                @"OVERSKRIFT",
                @"ID");
        }

        public void Insert(string connection, NyhedObject nyhed)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" INSERT INTO {0} (ID, FORFATTER, DATO, OVERSKRIFT, BESKRIVELSE, LINK, BILLEDMAPPE, BILLED1, BILLED2, BILLED3, VIDEO1, VIDEO2, VIDEO3, OFFENTLIG)", TableNameNyheder);
            sb.AppendFormat(@" VALUES (");
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Id.ToString(CultureInfo.InvariantCulture)));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Forfatter));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(nyhed.Dato)));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Overskrift));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Beskrivelse));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Link));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.BilledMappe));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Billed1));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Billed2));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Billed3));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Video1));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Video2));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(nyhed.Video3));
            sb.AppendFormat(@" {0})", InternalHelper.ValidateTextForMySql(Convert.ToSByte(nyhed.Offentlig).ToString(CultureInfo.InvariantCulture)));
            sb.AppendFormat(@" ;");
            sb.AppendFormat(@" SELECT MAX(ID) AS ID");
            sb.AppendFormat(@" FROM {0}", TableNameNyheder);
            sb.AppendFormat(@" ;");

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteScalar(connection, sb);
        }

        public void Update(string connection, NyhedObject nyhed)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" UPDATE {0} SET", TableNameNyheder);
            sb.AppendFormat(@" FORFATTER = {0},", InternalHelper.ValidateTextForMySql(nyhed.Forfatter));
            sb.AppendFormat(@" DATO = {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(nyhed.Dato)));
            sb.AppendFormat(@" OVERSKRIFT = {0},", InternalHelper.ValidateTextForMySql(nyhed.Overskrift));
            sb.AppendFormat(@" BESKRIVELSE = {0},", InternalHelper.ValidateTextForMySql(nyhed.Beskrivelse));
            sb.AppendFormat(@" LINK = {0},", InternalHelper.ValidateTextForMySql(nyhed.Link));
            sb.AppendFormat(@" BILLEDMAPPE = {0},", InternalHelper.ValidateTextForMySql(nyhed.BilledMappe));
            sb.AppendFormat(@" BILLED1 = {0},", InternalHelper.ValidateTextForMySql(nyhed.Billed1));
            sb.AppendFormat(@" BILLED2 = {0},", InternalHelper.ValidateTextForMySql(nyhed.Billed2));
            sb.AppendFormat(@" BILLED3 = {0},", InternalHelper.ValidateTextForMySql(nyhed.Billed3));
            sb.AppendFormat(@" VIDEO1 = {0},", InternalHelper.ValidateTextForMySql(nyhed.Video1));
            sb.AppendFormat(@" VIDEO2 = {0},", InternalHelper.ValidateTextForMySql(nyhed.Video2));
            sb.AppendFormat(@" VIDEO3 = {0},", InternalHelper.ValidateTextForMySql(nyhed.Video3));
            sb.AppendFormat(@" OFFENTLIG = {0}", InternalHelper.ValidateTextForMySql(Convert.ToSByte(nyhed.Offentlig).ToString(CultureInfo.InvariantCulture)));
            sb.AppendFormat(@" WHERE Id = {0}", InternalHelper.ValidateTextForMySql(nyhed.Id.ToString(CultureInfo.InvariantCulture)));

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }

        public void Delete(string connection, int id)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" DELETE");
            sb.AppendFormat(@" FROM {0}", TableNameNyheder);
            sb.AppendFormat(@" WHERE ID = {0}", id);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }
    }
}