using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using App_Code;
using App_Code.Model;
using Google.GData.Client;
using Model;
using MySql.Data.MySqlClient;

namespace Control
{
    public class BlogspotController
    {
        private const string TableNameBlogspot = "blogspot";

        public void Delete(string conn, int id)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" DELETE");
            sb.AppendFormat(@" FROM {0}", TableNameBlogspot);
            sb.AppendFormat(@" WHERE ID = {0}", id);

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteScalar(conn, sb);
        }

        public void Load(string connection, IPrincipal user, DropDownList dropdown)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" SELECT ID, Url");
            sb.AppendFormat(@" FROM {0}", TableNameBlogspot);
            if (!user.IsInRole(RollerEnum.Administrator.ToString()))
            {
                sb.AppendFormat(@" AND UPPER(USER) = {0}", InternalHelper.ValidateTextForMySql(user.Identity.Name).ToUpper());
            }
            sb.AppendFormat(@" ORDER BY DATE DESC");

            InternalHelper.DropDownListReader(
                connection,
                sb.ToString(),
                dropdown,
                @"URL",
                @"ID");
        }

        public int Insert(string conn, BlogspotObject blogspot)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" INSERT INTO {0} (USER, DATE, USERNAME, PASSWORD, URL)", TableNameBlogspot);
            sb.AppendFormat(@" VALUES(");
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(blogspot.User));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(blogspot.Date)));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(blogspot.Username));
            sb.AppendFormat(@" {0},", InternalHelper.ValidateTextForMySql(blogspot.Password));
            sb.AppendFormat(@" {0})", InternalHelper.ValidateTextForMySql(blogspot.Url));
            sb.AppendFormat(@" ;");
            sb.AppendFormat(@" SELECT MAX(Id)");
            sb.AppendFormat(@" FROM {0}", TableNameBlogspot);
            sb.AppendFormat(@" ;");

            var ctrl = new SysDatabaseController();
            return ObjectHelper.GetIntegerFromObject(ctrl.ExecuteScalar(conn, sb));
        }

        public void Update(string conn, BlogspotObject blogspot)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" UPDATE {0} SET", TableNameBlogspot);
            sb.AppendFormat(@" DATE = {0},", InternalHelper.ValidateTextForMySql(DateTimeHelper.DateToStringInDbFormat(blogspot.Date)));
            sb.AppendFormat(@" USERNAME = {0},", InternalHelper.ValidateTextForMySql(blogspot.Username));
            sb.AppendFormat(@" PASSWORD = {0},", InternalHelper.ValidateTextForMySql(blogspot.Password));
            sb.AppendFormat(@" URL = {0}", InternalHelper.ValidateTextForMySql(blogspot.Url));
            sb.AppendFormat(@" WHERE ID = {0}", blogspot.Id.ToString(CultureInfo.InvariantCulture));

            var ctrl = new SysDatabaseController();
            ctrl.ExecuteNonQuery(conn, sb);
        }

        public bool Validate(BlogspotObject blogspot)
        {
            try
            {
                var request = new BlogRequest(blogspot.Url, blogspot.Username, InternalHelper.DecryptString(blogspot.Password), 1);
                var query = new FeedQuery
                {
                    Uri = new Uri(string.Format(@"http://{0}/feeds/posts/default", request.Url)),
                    NumberToRetrieve = request.NumberToRetrieve
                };

                var service = new Service("blogger", "Spot Blogger")
                {
                    Credentials = new GDataCredentials(request.User, request.Pwd)
                };
                service.Query(query);
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Selecting single user object from own database, before calling blogspot.
        /// </summary>
        /// <param name="conn">Connection String</param>
        /// <param name="id">Object Id</param>
        /// <returns>Blogspot object ready to connect to blogspot webservice</returns>
        public BlogspotObject Select(string conn, int id)
        {
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(@" SELECT *");
                    sb.AppendFormat(@" FROM {0}", TableNameBlogspot);
                    sb.AppendFormat(@" WHERE ID = {0}", id);
                    var command = new MySqlCommand(sb.ToString(), connection);
                    return GetAll(command).First();
                }
            }
            catch (Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
            }
            return null;
        }

        public IEnumerable<BlogspotObject> Select(string conn)
        {
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    sb.AppendFormat(@" SELECT *");
                    sb.AppendFormat(@" FROM {0}", TableNameBlogspot);
                    return GetAll(new MySqlCommand(sb.ToString(), connection));
                }
            }
            catch (Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
            }
            return new List<BlogspotObject>();
        }

        public IEnumerable<BlogAtomicEntryContent> GetAtomicFeedEntryContent(IEnumerable<BlogRequest> blogRequests, int entries)
        {
            try
            {
                var result = new List<BlogAtomicEntryContent>();
                var atomFeeds = GetAtomFeedResultsFromTasks(blogRequests).Where(x => x != null);
                foreach (AtomFeed atomFeed in atomFeeds)
                {
                    for (int idx = 0; idx < atomFeed.Entries.Count; idx++)
                    {
                        var atomEntry = atomFeed.Entries[idx];
                        if (atomEntry.Title.Text.ToUpperInvariant().Trim() != "MMS") //ændring pr. 170715
                        {
                            var atomicEntry = new BlogAtomicEntryContent(atomEntry.Published, atomEntry.Title.Text, atomEntry.Content.Content, atomEntry.Content.Type);
                            result.Add(atomicEntry);
                        }
                    }
                }
                return result.OrderByDescending(x => x.Published);
            }
            catch (Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
            }
            return null;
        }

        #region Private Helpers

        private static IEnumerable<BlogspotObject> GetAll(MySqlCommand command)
        {
            var result = new List<BlogspotObject>();
            using (MySqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    var id = ObjectHelper.GetIntegerFromObject(reader["ID"]);
                    var user = ObjectHelper.GetStringFormObject(reader["USER"]);
                    var date = ObjectHelper.GetDateFromObject(reader["DATE"]);
                    var username = ObjectHelper.GetStringFormObject(reader["USERNAME"]);
                    var password = ObjectHelper.GetStringFormObject(reader["PASSWORD"]);
                    var url = ObjectHelper.GetStringFormObject(reader["URL"]);
                    result.Add(new BlogspotObject(id, user, date, username, password, url));
                }
            }
            return result;
        }

        private IEnumerable<AtomFeed> GetAtomFeedResultsFromTasks(IEnumerable<BlogRequest> blogRequests)
        {
            var result = new List<AtomFeed>();
            Parallel.ForEach(blogRequests, x => result.Add(GetAtomFeed(x)));
            return result;
        }

        private static AtomFeed GetAtomFeed(BlogRequest request)
        {
            try
            {
                var query = new FeedQuery
                {
                    Uri = new Uri(string.Format(@"http://{0}/feeds/posts/default", request.Url)),
                    NumberToRetrieve = request.NumberToRetrieve
                };

                var service = new Service("blogger", "Spot Blogger")
                                  {
                                      //Credentials = new GDataCredentials(request.User, request.Pwd) //
                                  };
                return service.Query(query);
            }
            catch (Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
                return null;
            }
        }

        #endregion
    }
}