using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;
using MySql.Data.MySqlClient;

namespace Control
{
    public class FangsterController
    {
        private const string TableNameFangster = "fangster";
        private const string TableNameArter = "arter";

        public string GetList(string brugerId, int number, string artId, bool dk)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" SELECT @ROWNUM:=@ROWNUM + 1 AS 'NR', X.*");
            sb.AppendFormat(" FROM (");
            sb.AppendFormat(" SELECT *");
            sb.AppendFormat(" FROM {0}", TableNameFangster);
            sb.AppendFormat(" WHERE 1 = 1");
            if (!string.IsNullOrEmpty(brugerId))
            {
                sb.AppendFormat(" AND FANGER = '{0}'", brugerId);
            }
            sb.AppendFormat(" AND ART = (SELECT NAVN FROM {0} WHERE ARTID = {1})", TableNameArter, artId);
            if(dk)
            {
                sb.AppendFormat(" AND LAND = 'DANMARK'");
            }
            else
            {
                sb.AppendFormat(" AND LAND <> 'DANMARK'");
            }
            sb.AppendFormat(" ORDER BY VAEGT DESC");
            sb.AppendFormat(" LIMIT 0, {0}", number);
            sb.AppendFormat(" ) X, (SELECT @ROWNUM:=0) Y");
            return sb.ToString();
        }

        public FangsterObject Get(string conn, int id)
        {
            FangsterObject result = null;
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(@" SELECT *");
                    sb.AppendFormat(@" FROM {0}", TableNameFangster);
                    sb.AppendFormat(@" WHERE FANGSTID = {0}", id);

                    var command = new MySqlCommand(sb.ToString(), connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var art = ObjectHelper.GetStringFormObject(reader["ART"]);
                            var beskrivelse = ObjectHelper.GetStringFormObject(reader["BESKRIVELSE"]);
                            var billed1 = ObjectHelper.GetStringFormObject(reader["BILLED1"]);
                            var billed2 = ObjectHelper.GetStringFormObject(reader["BILLED2"]);
                            var billed3 = ObjectHelper.GetStringFormObject(reader["BILLED3"]);
                            var billedMappe = ObjectHelper.GetStringFormObject(reader["BILLEDMAPPE"]);
                            var dato = ObjectHelper.GetDateFromObject(reader["DATO"]);
                            var fanger = ObjectHelper.GetStringFormObject(reader["FANGER"]);
                            var fangstId = id;
                            var fiskevand = ObjectHelper.GetStringFormObject(reader["FISKEVAND"]);
                            var forfatter = ObjectHelper.GetStringFormObject(reader["FORFATTER"]);
                            var laengde = ObjectHelper.GetDoubleFromObject(reader["LAENGDE"]);
                            var land = ObjectHelper.GetStringFormObject(reader["LAND"]);
                            var link = ObjectHelper.GetStringFormObject(reader["LINK"]);
                            var offentlig = ObjectHelper.GetBooleanFromObject(reader["OFFENTLIG"]);
                            var placering = ObjectHelper.GetStringFormObject(reader["PLACERING"]);
                            var redskab = ObjectHelper.GetStringFormObject(reader["REDSKAB"]);
                            var vaegt = ObjectHelper.GetDoubleFromObject(reader["VAEGT"]);
                            var vejr = ObjectHelper.GetStringFormObject(reader["VEJR"]);
                            var video1 = ObjectHelper.GetStringFormObject(reader["VIDEO1"]);
                            var video2 = ObjectHelper.GetStringFormObject(reader["VIDEO2"]);
                            var video3 = ObjectHelper.GetStringFormObject(reader["VIDEO3"]);
                            var yearFish = ObjectHelper.GetBooleanFromObject(reader["YEARFISH"]);
                            result = new FangsterObject(fangstId, forfatter, dato, art, vaegt, laengde, fanger, fiskevand, placering, land, redskab, vejr, beskrivelse, link, billedMappe, billed1, billed2, billed3, video1, video2, video3, offentlig, yearFish);
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
            sb.AppendFormat(@" SELECT FANGSTID, TRIM(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(DATO,'  '),ART),' / '),VAEGT),' / '),FANGER)) AS OVERSKRIFT");
            sb.AppendFormat(@" FROM {0}", TableNameFangster);
            if (!user.IsInRole(RollerEnum.Administrator.ToString()))
            {
                sb.AppendFormat(@" WHERE UPPER(FORFATTER) = {0}", InternalHelper.ValidateTextForMySql(user.Identity.Name).ToUpper());
            }
            sb.AppendFormat(@" ORDER BY DATO DESC");

            InternalHelper.DropDownListReader(
                connection,
                sb.ToString(),
                dropdown,
                @"OVERSKRIFT",
                @"FANGSTID");
        }

        public int Insert(string connection, FangsterObject fangster)
        {
            int newId = NextId(connection);

            var sb = new StringBuilder();
            sb.AppendFormat(@" INSERT INTO {0} (FANGSTID, FORFATTER, DATO, ART, VAEGT, LAENGDE, FANGER, FISKEVAND, PLACERING, LAND, REDSKAB, VEJR, BESKRIVELSE, LINK, BILLEDMAPPE, BILLED1, BILLED2, BILLED3, OFFENTLIG, YEARFISH)", TableNameFangster);
            sb.AppendFormat(@" VALUES(");
            sb.AppendFormat(@"{0},", newId);
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Forfatter));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(fangster.Dato)));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Art));
            sb.AppendFormat(@"{0},", fangster.Vaegt.ToString(CultureInfo.InvariantCulture));
            sb.AppendFormat(@"{0},", fangster.Laengde.ToString(CultureInfo.InvariantCulture));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Fanger));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Fiskevand));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Placering));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Land));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Redskab));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Vejr));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Beskrivelse));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(InternalHelper.CheckLinkAddress(fangster.Link)));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.BilledMappe));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Billed1));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Billed2));
            sb.AppendFormat(@"{0},", InternalHelper.ValidateTextForMySql(fangster.Billed3));
            sb.AppendFormat(@"{0},", Convert.ToByte(fangster.Offentlig));
            sb.AppendFormat(@"{0})", Convert.ToByte(fangster.YearFish));

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);

            return newId;
        }

        public void Update(string connection, FangsterObject fangster)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" UPDATE {0} SET", TableNameFangster);
            sb.AppendFormat(@" FORFATTER = {0},", InternalHelper.ValidateTextForMySql(fangster.Forfatter));
            sb.AppendFormat(@" DATO = {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(fangster.Dato)));
            sb.AppendFormat(@" ART = {0},", InternalHelper.ValidateTextForMySql(fangster.Art));
            sb.AppendFormat(@" VAEGT = {0},", fangster.Vaegt.ToString(CultureInfo.InvariantCulture));
            sb.AppendFormat(@" LAENGDE = {0},", fangster.Laengde.ToString(CultureInfo.InvariantCulture));
            sb.AppendFormat(@" FANGER = {0},", InternalHelper.ValidateTextForMySql(fangster.Fanger));
            sb.AppendFormat(@" FISKEVAND = {0},", InternalHelper.ValidateTextForMySql(fangster.Fiskevand));
            sb.AppendFormat(@" PLACERING = {0},", InternalHelper.ValidateTextForMySql(fangster.Placering));
            sb.AppendFormat(@" LAND = {0},", InternalHelper.ValidateTextForMySql(fangster.Land));
            sb.AppendFormat(@" REDSKAB = {0},", InternalHelper.ValidateTextForMySql(fangster.Redskab));
            sb.AppendFormat(@" VEJR = {0},", InternalHelper.ValidateTextForMySql(fangster.Vejr));
            sb.AppendFormat(@" BESKRIVELSE = {0},", InternalHelper.ValidateTextForMySql(fangster.Beskrivelse));
            sb.AppendFormat(@" LINK = {0},", InternalHelper.ValidateTextForMySql(InternalHelper.CheckLinkAddress(fangster.Link)));
            sb.AppendFormat(@" BILLEDMAPPE = {0},", InternalHelper.ValidateTextForMySql(fangster.BilledMappe));
            sb.AppendFormat(@" BILLED1 = {0},", InternalHelper.ValidateTextForMySql(fangster.Billed1));
            sb.AppendFormat(@" BILLED2 = {0},", InternalHelper.ValidateTextForMySql(fangster.Billed2));
            sb.AppendFormat(@" BILLED3 = {0},", InternalHelper.ValidateTextForMySql(fangster.Billed3));
            sb.AppendFormat(@" OFFENTLIG = {0},", Convert.ToByte(fangster.Offentlig));
            sb.AppendFormat(@" YEARFISH = {0}", Convert.ToByte(fangster.YearFish));
            sb.AppendFormat(@" WHERE FANGSTID = {0}", fangster.FangstId.ToString(CultureInfo.InvariantCulture));
            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }

        public void Delete(string connection, int id)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" DELETE");
            sb.AppendFormat(@" FROM {0}", TableNameFangster);
            sb.AppendFormat(@" WHERE FANGSTID = {0}", id);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }

        private int NextId(string connection)
        {
            string command = string.Format("SELECT MAX(FANGSTID) + 1 FROM {0}", TableNameFangster);

            var ctrl = new SysDatabaseController();
            object result = ctrl.ExecuteScalar(connection, command);

            return ObjectHelper.GetIntegerFromObject(result);
        }

        /// <summary>
        /// Counting the use of 'ArterObject.Navn' in 'FangsterObject.Art'
        /// </summary>
        /// <param name="connection">Standard connection string</param>
        /// <param name="art">ArterObject</param>
        /// <returns>Count of Use as Int32</returns>
        public int CountUseOfArt(string connection, ArterObject art)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" SELECT COUNT(*)");
            sb.AppendFormat(@" FROM {0}", TableNameFangster);
            sb.AppendFormat(@" WHERE ART = {0}", InternalHelper.ValidateTextForMySql(art.Navn));

            var ctrl = new SysDatabaseController();
            var scalar = ctrl.ExecuteScalar(connection, sb);
            return ObjectHelper.GetIntegerFromObject(scalar);
        }

        public string GetTrohpies(int year)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" SELECT A.*");
            sb.AppendFormat(" FROM (");
            sb.AppendFormat(" SELECT T0.FANGSTID, T0.ART, T0.DATO, T0.VAEGT, T0.FISKEVAND, T0.BILLEDMAPPE, T0.BILLED1");
            sb.AppendFormat(" FROM {0} T0", TableNameFangster);
            sb.AppendFormat(" JOIN {0} T1 ON T1.NAVN = T0.ART AND T1.ARTID = 2", TableNameArter);
            sb.AppendFormat(" WHERE YEAR(T0.DATO) = {0}", year);
            sb.AppendFormat(" ORDER BY VAEGT DESC");
            sb.AppendFormat(" LIMIT 1");
            sb.AppendFormat(" ) A");
            sb.AppendFormat(" UNION");
            sb.AppendFormat(" SELECT B.*");
            sb.AppendFormat(" FROM (");
            sb.AppendFormat(" SELECT T0.FANGSTID, T0.ART, T0.DATO, T0.VAEGT, T0.FISKEVAND, T0.BILLEDMAPPE, T0.BILLED1");
            sb.AppendFormat(" FROM {0} T0", TableNameFangster);
            sb.AppendFormat(" JOIN {0} T1 ON T1.NAVN = T0.ART AND T1.ARTID = 3", TableNameArter);
            sb.AppendFormat(" WHERE YEAR(T0.DATO) = {0}", year);
            sb.AppendFormat(" ORDER BY VAEGT DESC");
            sb.AppendFormat(" LIMIT 1");
            sb.AppendFormat(" ) B");
            sb.AppendFormat(" UNION");
            sb.AppendFormat(" SELECT C.*");
            sb.AppendFormat(" FROM (");
            sb.AppendFormat(" SELECT T0.FANGSTID, T0.ART, T0.DATO, T0.VAEGT, T0.FISKEVAND, T0.BILLEDMAPPE, T0.BILLED1");
            sb.AppendFormat(" FROM {0} T0", TableNameFangster);
            sb.AppendFormat(" JOIN {0} T1 ON T1.NAVN = T0.ART AND T1.ARTID = 17", TableNameArter);
            sb.AppendFormat(" WHERE YEAR(T0.DATO) = {0}", year);
            sb.AppendFormat(" ORDER BY VAEGT DESC");
            sb.AppendFormat(" LIMIT 1");
            sb.AppendFormat(" ) C");
            sb.AppendFormat(" UNION");
            sb.AppendFormat(" SELECT D.*");
            sb.AppendFormat(" FROM (");
            sb.AppendFormat(" SELECT T0.FANGSTID, T0.ART, T0.DATO, T0.VAEGT, T0.FISKEVAND, T0.BILLEDMAPPE, T0.BILLED1");
            sb.AppendFormat(" FROM {0} T0", TableNameFangster);
            sb.AppendFormat(" JOIN {0} T1 ON T1.NAVN = T0.ART AND T1.ARTID = 21", TableNameArter);
            sb.AppendFormat(" WHERE YEAR(T0.DATO) = {0}", year);
            sb.AppendFormat(" ORDER BY VAEGT DESC");
            sb.AppendFormat(" LIMIT 1");
            sb.AppendFormat(" ) D");
            return sb.ToString();
        }
    }
}