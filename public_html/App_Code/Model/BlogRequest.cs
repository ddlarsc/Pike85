namespace Model
{
    public class BlogRequest
    {
        public string Url { get; private set; }
        public string User { get; private set; }
        public string Pwd { get; private set; }
        public int NumberToRetrieve { get; private set; }

        public BlogRequest(string url, string user, string pwd, int numberToRetrieve)
        {
            Url = url;
            User = user;
            Pwd = pwd;
            NumberToRetrieve = numberToRetrieve;
        }

        public bool Valid()
        {
            if (!string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Pwd) && NumberToRetrieve > 0)
            {
                return true;
            }
            return false;
        }
    }
}