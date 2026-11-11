using System.Net.Mail;

namespace App_Code.Model
{
    public class MailMessageObject
    {
        public string MailAddressFrom { get; private set; }
        public string MailAddressTo { get; private set; }
        public bool IsBodyHtml { get; private set; }
        public MailPriority MailPriority { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        public MailMessageObject(string mailAddressFrom, string mailAddressTo, bool isBodyHtml, MailPriority mailPriority, string subject, string body)
        {
            MailAddressFrom = mailAddressFrom;
            MailAddressTo = mailAddressTo;
            IsBodyHtml = isBodyHtml;
            MailPriority = mailPriority;
            Subject = subject;
            Body = body;
        }
    }
}