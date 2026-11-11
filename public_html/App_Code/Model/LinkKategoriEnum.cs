using System.ComponentModel;

namespace App_Code.Model
{
    public enum LinkKategoriEnum
    {
        [Description("-- Vælg --")]
        None = 0,
        [Description("Fiskeri")]
        Fiskeri = 1,
        [Description("Vejr")]
        Vejr = 2,
        [Description("Turbåde")]
        Turbåde = 3,
        [Description("Webbutikker")]
        Webbutikker = 4,
        [Description("Forums")]
        Forum = 5,
        [Description("Diverse")]
        Diverse = 6,
    }
}