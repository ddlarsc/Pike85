using System.Net.Mail;
using System.Web.Security;
using System.Web.UI.WebControls;
using System;
using System.Globalization;
using App_Code;
using App_Code.Model;
using Control;
using Model;

namespace Control
{
    /// <summary>
    /// Summary description for SysMailController
    /// </summary>
    public class SysMailController
    {
        public void Send(MailMessageObject mailMessageObject)
        {
            var tmpMailAddressFrom = mailMessageObject.MailAddressFrom;
            var tmpMailAddressTo = mailMessageObject.MailAddressTo;
            if(string.IsNullOrEmpty(tmpMailAddressFrom) || string.IsNullOrEmpty(tmpMailAddressTo))
            {
                return;
            }

            var mailAddressFrom = new MailAddress(tmpMailAddressFrom);
            var mailAddressTo = new MailAddress(tmpMailAddressTo);
            var mailMessage = new MailMessage(mailAddressFrom,mailAddressTo)
                                  {
                                      Body = mailMessageObject.Body,
                                      IsBodyHtml = mailMessageObject.IsBodyHtml,
                                      Priority = mailMessageObject.MailPriority,
                                      Subject = mailMessageObject.Subject
                                  };

            var smtpClient = new SmtpClient
            {
                Host = Resources.Pike85.MailAdviceSmtp,
                Port = 587 
            };
            smtpClient.Credentials = new System.Net.NetworkCredential(Resources.Pike85.SMTPMailSendAddress, Resources.Pike85.SMTPMail);
            smtpClient.Send(mailMessage);
        }
    }
}