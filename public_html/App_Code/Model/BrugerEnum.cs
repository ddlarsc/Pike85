using System.ComponentModel;

namespace App_Code.Model
{
    public enum BrugerEnum
    {
        [Description("Neutral Type")]
        None = 0,

        [Description("Creat Type")]
        Create = 1,

        [Description("Update Type")]
        Update = 2,
    }
}