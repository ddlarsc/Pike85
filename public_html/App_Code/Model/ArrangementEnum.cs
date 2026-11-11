using System.ComponentModel;

namespace App_Code.Model
{
    public enum ArrangementEnum
    {
        [Description("- Vælg -")]
        None,
        [Description("Morgen")]
        Morgen,
        [Description("Til morgenmad")]
        Morgenmad,
        [Description("Formiddag")]
        Formiddag,
        [Description("Middag")]
        Middag,
        [Description("Eftermiddag")]
        Eftermiddag,
        [Description("Til aftensmad")]
        Aftensmad,
        [Description("Aften")]
        Aften,
        [Description("Nat")]
        Nat,
    }
}