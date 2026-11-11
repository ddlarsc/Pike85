using System.ComponentModel;

namespace App_Code.Model
{
    public enum EmailSubjectEnum
    {
        [Description("Empty")]
        None = 0,
        [Description("Besked om oprettelsen af et nyt arrangement i Pike85")]
        CreateArragement = 1,
        [Description("Besked om ændringer i et eksisterende arrangement i Pike85")]
        UpdateArrangement = 2,
        [Description("Besked om sletning af et arrangement i Pike85")]
        DeleteArrangement = 3
    }
}