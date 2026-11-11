using System.ComponentModel;

namespace App_Code.Model
{
    public enum LinkLandEnum
    {
        [Description("-- Vælg --")]
        None = 0,
        [Description("Danmark")]
        Danmark = 1,
        [Description("Sverige")]
        Sverige = 2,
        [Description("Norge")]
        Norge = 3,
        [Description("USA/Canada/m.fl.")]
        Nordamerika = 4,
        [Description("Tyskland")]
        Tyskland = 5,
        [Description("Diverse")]
        Diverse = 6,
    }
}