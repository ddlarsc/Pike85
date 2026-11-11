using System.Text;
using MySql.Data.MySqlClient;
using System;

namespace Control
{
    public class SysDatabaseController
    {
        public void ExecuteNonQuery(string conn, StringBuilder arg)
        {
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var command = new MySqlCommand(arg.ToString(), connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
            }
        }

        public object ExecuteScalar(string conn, string arg)
        {
            object result = null;
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var command = new MySqlCommand(arg, connection);
                    result = command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
            }
            return result;
        }

        public object ExecuteScalar(string conn, StringBuilder arg)
        {
            object result = null;
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var command = new MySqlCommand(arg.ToString(), connection);
                    result = command.ExecuteScalar();
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
