using System.ComponentModel;

namespace App_Code.Model
{
    public enum RollerEnum
    {
        [Description("-- Vælg --")]
        None = 0, 
        [Description("Medlem")]
        Medlem = 1,
        [Description("Administrator")]
        Administrator = 2,
        [Description("Anonym")]
        Anonym = 3,
    }
}