namespace App_Code.Model
{
    public class LinkObject
    {
        public int Id {get; private set;}
        public string Forfatter {get; private set;}
        public string Navn {get; private set;}
        public string Adresse {get; private set;}
        public string Beskrivelse {get; private set;}
        public string Kategori {get; private set;}
        public string Land {get; private set;}
        public string Billed {get; private set;}

        public LinkObject(int id, string forfatter, string navn, string adresse, string beskrivelse, string kategori, string land, string billede)
        {
            Id = id;
            Forfatter = forfatter;
            Navn = navn;
            Adresse = adresse;
            Beskrivelse = beskrivelse;
            Kategori = kategori;
            Land = land;
            Billed = billede;
        }
    }
}