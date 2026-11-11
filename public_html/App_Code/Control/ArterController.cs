using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;

namespace Control
{
    public class ArterController
    {
        private const string TableNameArter = "arter";

        public void Get(string connection, DropDownList list)
        {
            string commandText = string.Format("SELECT NAVN, ARTID FROM {0} ORDER BY NAVN ASC", TableNameArter);
            InternalHelper.DropDownListReader(connection, commandText, list, "NAVN", "ARTID");
        }

        public void Insert(string connection, ArterObject art)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" INSERT INTO {0} (ARTID, NAVN)", TableNameArter);
            sb.AppendFormat(" VALUES (");
            sb.AppendFormat(" {0},", art.ArtId);
            sb.AppendFormat(" {0})", InternalHelper.ValidateTextForMySql(art.Navn));

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }

        public void Update(string connection, ArterObject art)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" UPDATE {0} SET ", TableNameArter);
            sb.AppendFormat(" NAVN = {0}", InternalHelper.ValidateTextForMySql(art.Navn));
            sb.AppendFormat(" WHERE ARTID = {0}", art.ArtId.ToString(CultureInfo.InvariantCulture));

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }

        public void Delete(string connection, ArterObject art)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" DELETE");
            sb.AppendFormat(" FROM {0}", TableNameArter);
            sb.AppendFormat(" WHERE ARTID = {0}", art.ArtId);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(connection, sb);
        }

        public int NextId(string connection)
        {
            var ctrl = new SysDatabaseController();
            object result = ctrl.ExecuteScalar(connection, string.Format("SELECT MAX(ARTID) + 1 FROM {0}", TableNameArter));

            return ObjectHelper.GetIntegerFromObject(result);
        }
    }
}