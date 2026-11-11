using System;
using System.Text;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;
using MySql.Data.MySqlClient;

namespace Control
{
    public class BrugerController
    {
        const string TableNameBruger = "bruger";

        public void GetByIdAndKortnavn(string conn, DropDownList ctrl)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" SELECT BRUGERID, KORTNAVN");
            sb.AppendFormat(" FROM {0}", TableNameBruger);
            sb.AppendFormat(" ORDER BY BRUGERID ASC");

            InternalHelper.DropDownListReader(conn, sb.ToString(), ctrl, "KORTNAVN", "BRUGERID");
        }

        public void Get(string conn, DropDownList ctrl)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" SELECT BRUGERID");
            sb.AppendFormat(" FROM {0}", TableNameBruger);
            sb.AppendFormat(" ORDER BY BRUGERID ASC");

            InternalHelper.DropDownListReader(conn, sb.ToString(), ctrl, "BRUGERID");
        }

        public BrugerObject Get(string conn, string userId)
        {
            BrugerObject result = null;
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(" SELECT *");
                    sb.AppendFormat(" FROM {0}", TableNameBruger);
                    sb.AppendFormat(" WHERE BRUGERID='{0}'", userId);

                    var command = new MySqlCommand(sb.ToString(), connection);
                    var reader = command.ExecuteReader();                    
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var birthday = ObjectHelper.GetDateFromObject(reader["FOEDSELSDAG"]);
                            var city = reader["BY"].ToString();
                            var dead = ObjectHelper.GetBooleanFromObject(reader["DEAD"]);
                            var description = reader["BESKRIVELSE"].ToString();
                            var email1 = reader["EMAIL"].ToString();
                            var email2 = reader["EMAIL2"].ToString();
                            var enrolled = ObjectHelper.GetDateFromObject(reader["INDMELDT"]);
                            var image = string.Format("~/Billeder/Medlemmer/{0}.gif", reader["BILLED"]);
                            var name = reader["NAVN"].ToString();
                            var phoneMobil = reader["MOBILTLF"].ToString();
                            var phonePrivate = reader["HJEMTLF"].ToString();
                            var phoneWork = reader["ARBTLF"].ToString();
                            var isPublic = ObjectHelper.GetBooleanFromObject(reader["OFFENTLIG"]);
                            var shortName = reader["KORTNAVN"].ToString();
                            var street = reader["ADRESSE"].ToString();
                            var userCode = reader["KODEORD"].ToString();
                            var userLevel = reader["ROLLER"].ToString();
                            var zipCode = reader["POSTNR"].ToString();
                            result = new BrugerObject(userId, userCode, name, shortName, birthday, description, email1, image, userLevel, isPublic, street, city, zipCode, email2, phonePrivate, phoneWork, phoneMobil, enrolled, dead);
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

        public void Update(string conn, BrugerObject bruger)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" UPDATE {0} SET", TableNameBruger);
            sb.AppendFormat(" FOEDSELSDAG = {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(bruger.Birthday)));
            sb.AppendFormat(" {0}.BY = {1},", TableNameBruger, InternalHelper.ValidateTextForMySql(bruger.City));
            sb.AppendFormat(" DEAD = '{0}',", bruger.Dead ? 1 : 0);
            sb.AppendFormat(" BESKRIVELSE = {0},", InternalHelper.ValidateTextForMySql(bruger.Description));
            sb.AppendFormat(" EMAIL = {0},", InternalHelper.ValidateTextForMySql(bruger.Email1));
            sb.AppendFormat(" EMAIL2 = {0},", InternalHelper.ValidateTextForMySql(bruger.Email2));
            sb.AppendFormat(" INDMELDT = {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(bruger.Enrolled)));
            sb.AppendFormat(" BILLED = {0},", InternalHelper.ValidateTextForMySql(bruger.Image));
            sb.AppendFormat(" NAVN = {0},", InternalHelper.ValidateTextForMySql(bruger.Name));
            sb.AppendFormat(" MOBILTLF = {0},", InternalHelper.ValidateTextForMySql(bruger.PhoneMobil));
            sb.AppendFormat(" HJEMTLF = {0},", InternalHelper.ValidateTextForMySql(bruger.PhonePrivate));
            sb.AppendFormat(" ARBTLF = {0},", InternalHelper.ValidateTextForMySql(bruger.PhoneWork));
            sb.AppendFormat(" OFFENTLIG = '{0}',", bruger.Public ? 1 : 0);
            sb.AppendFormat(" KORTNAVN = {0},", InternalHelper.ValidateTextForMySql(bruger.ShortName));
            sb.AppendFormat(" ADRESSE = {0},", InternalHelper.ValidateTextForMySql(bruger.Street));
            //user.UserCode
            sb.AppendFormat(" ROLLER = {0},", InternalHelper.ValidateTextForMySql(bruger.UserLevel));
            //user.UserId
            sb.AppendFormat(" POSTNR = {0}", InternalHelper.ValidateTextForMySql(bruger.ZipCode));
            sb.AppendFormat(" WHERE BRUGERID = '{0}'", bruger.UserId);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(conn, sb);
        }

        public void Create(string conn, BrugerObject bruger)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" INSERT INTO {0} (FOEDSELSDAG, {0}.BY, DEAD, BESKRIVELSE, EMAIL, EMAIL2, INDMELDT, BILLED, NAVN, MOBILTLF, HJEMTLF, ARBTLF, OFFENTLIG, KORTNAVN, ADRESSE, KODEORD, ROLLER, BRUGERID, POSTNR)", TableNameBruger);
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(bruger.Birthday)));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.City));
            sb.AppendFormat(" {0},", bruger.Dead ? 1 : 0);
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.Description));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.Email1));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.Email2));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(bruger.Enrolled)));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.Image));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.Name));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.PhoneMobil));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.PhonePrivate));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.PhoneWork));
            sb.AppendFormat(" '{0}',", bruger.Public ? 1 : 0);
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.ShortName));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.Street));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.UserCode));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.UserLevel));
            sb.AppendFormat(" {0},", InternalHelper.ValidateTextForMySql(bruger.UserId));
            sb.AppendFormat(" {0} ", InternalHelper.ValidateTextForMySql(bruger.ZipCode));
            sb.AppendFormat(" )");

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(conn, sb);
        }

        public void Delete(string conn, string userId)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" DELETE");
            sb.AppendFormat(" FROM {0}", TableNameBruger);
            sb.AppendFormat(" WHERE BRUGERID = '{0}'", userId);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(conn, sb);
        }
    }
}