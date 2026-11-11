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
    public class LinkController
    {
        private const string TableNameLinks = "links";

        public LinkObject Get(string conn, int id)
        {
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(@" SELECT *");
                    sb.AppendFormat(@" FROM {0}", TableNameLinks);
                    sb.AppendFormat(@" WHERE ID = {0}", id);

                    var command = new MySqlCommand(sb.ToString(), connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var forfatter = ObjectHelper.GetStringFormObject(reader["FORFATTER"]);
                            var navn = ObjectHelper.GetStringFormObject(reader["NAVN"]);
                            var adresse = ObjectHelper.GetStringFormObject(reader["ADRESSE"]);
                            var beskrivelse = ObjectHelper.GetStringFormObject(reader["BESKRIVELSE"]);
                            var kategori = ObjectHelper.GetStringFormObject(reader["KATEGORI"]);
                            var land = ObjectHelper.GetStringFormObject(reader["LAND"]);
                            var billed = ObjectHelper.GetStringFormObject(reader["BILLED"]);
                            return new LinkObject(id, forfatter, navn, adresse, beskrivelse, kategori, land, billed);
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

        [Obsolete("Bør slettes efter 2014, hvis ikke taget i brug. LCD 02.01.14", true)]
        public List<LinkObject> Get(string conn)
        {
            var result = new List<LinkObject>();
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(@" SELECT *");
                    sb.AppendFormat(@" FROM {0}", TableNameLinks);

                    var command = new MySqlCommand(sb.ToString(), connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = ObjectHelper.GetIntegerFromObject(reader["ID"]);
                            var forfatter = ObjectHelper.GetStringFormObject(reader["FORFATTER"]);
                            var navn = ObjectHelper.GetStringFormObject(reader["NAVN"]);
                            var adresse = ObjectHelper.GetStringFormObject(reader["ADRESSE"]);
                            var beskrivelse = ObjectHelper.GetStringFormObject(reader["BESKRIVELSE"]);
                            var kategori = ObjectHelper.GetStringFormObject(reader["KATEGORI"]);
                            var land = ObjectHelper.GetStringFormObject(reader["LAND"]);
                            var billed = ObjectHelper.GetStringFormObject(reader["BILLED"]);
                            result.Add(new LinkObject(id, forfatter, navn, adresse, beskrivelse, kategori, land, billed));
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

        public void Get(string connection, IPrincipal user, DropDownList dropDownList)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" SELECT ID, CONCAT(ID, CONCAT(' ', NAVN)) AS OVERSKRIFT");
            sb.AppendFormat(@" FROM {0}", TableNameLinks);
            if (!user.IsInRole(RollerEnum.Administrator.ToString()))
            {
                sb.AppendFormat(@" WHERE UPPER(FORFATTER) = {0}", InternalHelper.ValidateTextForMySql(user.Identity.Name).ToUpper());
            }
            sb.AppendFormat(@" ORDER BY ID DESC");

            InternalHelper.DropDownListReader(
                connection,
                sb.ToString(),
                dropDownList,
                @"OVERSKRIFT",
                @"ID");
        }

        public int Insert(string connection, LinkObject link)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" INSERT INTO {0} (ID, FORFATTER, NAVN, ADRESSE, BESKRIVELSE, KATEGORI, LAND, BILLED)", TableNameLinks);
            sb.AppendFormat(@" VALUES (");
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(link.Id.ToString(CultureInfo.InvariantCulture)));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(link.Forfatter));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(link.Navn));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(link.Adresse));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(link.Beskrivelse));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(link.Kategori));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(link.Land));
            sb.AppendFormat(@" {0})", InternalHelper.ValidateTextForMySql(link.Billed));
            sb.AppendFormat(@" ;");
            sb.AppendFormat(@" SELECT MAX(ID) AS ID");
            sb.AppendFormat(@" FROM {0}", TableNameLinks);
            sb.AppendFormat(@" ;");

            var ctrl = new SysDatabaseController();
            return ObjectHelper.GetIntegerFromObject(ctrl.ExecuteScalar(connection, sb));
        }

        public void Update(string connection, LinkObject link)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" UPDATE {0} SET", TableNameLinks);
            sb.AppendFormat(@" ID = {0},", InternalHelper.ValidateTextForMySql(link.Id.ToString(CultureInfo.InvariantCulture)));
            sb.AppendFormat(@" FORFATTER = {0},", InternalHelper.ValidateTextForMySql(link.Forfatter));
            sb.AppendFormat(@" NAVN = {0},", InternalHelper.ValidateTextForMySql(link.Navn));
            sb.AppendFormat(@" ADRESSE = {0},", InternalHelper.ValidateTextForMySql(link.Adresse));
            sb.AppendFormat(@" BESKRIVELSE = {0},", InternalHelper.ValidateTextForMySql(link.Beskrivelse));
            sb.AppendFormat(@" KATEGORI = {0},", InternalHelper.ValidateTextForMySql(link.Kategori));
            sb.AppendFormat(@" LAND = {0},", InternalHelper.ValidateTextForMySql(link.Land));
            sb.AppendFormat(@" BILLED = {0}", InternalHelper.ValidateTextForMySql(link.Billed));
            sb.AppendFormat(@" WHERE ID = {0}", InternalHelper.ValidateTextForMySql(link.Id.ToString(CultureInfo.InvariantCulture)));

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }

        public void Delete(string connection, int id)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" DELETE");
            sb.AppendFormat(@" FROM {0}", TableNameLinks);
            sb.AppendFormat(@" WHERE ID = {0}", id);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }
    }
}