using System;
using System.Text;

namespace Model
{
    public class BlogspotObject
    {
        public int Id { get; private set; }

        public string User { get; private set; }

        public DateTime Date { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string Url { get; private set; }

        public BlogspotObject(int id, string user, DateTime date, string username, string password, string url)
        {
            Id = id;
            User = user;
            Date = date;
            Username = username;
            Password = password;
            Url = url;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat(" Id [{0}]{1}", Id, Environment.NewLine);
            sb.AppendFormat(" User [{0}]{1}", User, Environment.NewLine);
            sb.AppendFormat(" Date [{0}]{1}", Date, Environment.NewLine);
            sb.AppendFormat(" Username [{0}]{1}", Username, Environment.NewLine);
            sb.AppendFormat(" Password [{0}]{1}", Password, Environment.NewLine);
            sb.AppendFormat(" Url [{0}]{1}", Url, Environment.NewLine);
            return sb.ToString();
        }
    }
}