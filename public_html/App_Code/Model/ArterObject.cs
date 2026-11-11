using System.ComponentModel;

namespace App_Code.Model
{
    public class ArterObject
    {
        [Description("ArtId / smallint(6)")]
        public int ArtId {get; set;}

        [Description("Navn / varchar(100)")]
        public string Navn {get; set;}

        public ArterObject(int artId, string navn)
        {
            ArtId = artId;
            Navn = navn;
        }
    }
}