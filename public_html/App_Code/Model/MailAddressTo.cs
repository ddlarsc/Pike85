
using System.ComponentModel;

namespace Model
{
    public enum MailAddressTo
    {
        [Description("")]
        Ingen = 0,
        [Description("webmaster@pike85.dk")]
        Administrator = 1,
        [Description("pike85@pike85.dk")]
        Alle  = 2,
        [Description("exception@pike85.dk")]
        Exception = 3,
    }
}