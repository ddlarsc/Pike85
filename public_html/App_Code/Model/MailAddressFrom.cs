using System.ComponentModel;

namespace App_Code.Model
{
    public enum MailAddressFrom
    {
        [Description("Empty")]
        None = 0,
        [Description("noreply@pike85.dk")]
        NoReply = 1,
    }
}