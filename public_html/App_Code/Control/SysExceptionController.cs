using System;
using System.Net.Mail;
using App_Code;
using App_Code.Model;
using Model;

namespace Control
{
    /// <summary>
    /// Summary description for SysExceptionController
    /// </summary>
    public class SysExceptionController
    {
        public void OnException(Exception ex, int counter)
        {
            if(counter < 2)
            {
                try
                {
                    var mailMessageObject = new MailMessageObject(
                        EnumHelper.GetDescription(MailAddressFrom.NoReply),
                        EnumHelper.GetDescription(MailAddressTo.Exception),
                        false,
                        new MailPriority(),
                        "Der er kastet en Exception!",
                        ex.StackTrace
                        );

                    var ctrl = new SysMailController();
                    ctrl.Send(mailMessageObject);
                }
                catch(Exception)
                {
                    counter++;
                    OnException(ex, counter);
                }
            }
        }
    }
}
