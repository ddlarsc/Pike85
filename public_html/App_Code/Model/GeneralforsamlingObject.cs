using System;

namespace App_Code.Model
{
    public class GeneralforsamlingObject
    {
        public int Pk { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Link { get; set; }
        public string LinkText { get; set; }
        public string Footer { get; set; }
        public string Signature { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
