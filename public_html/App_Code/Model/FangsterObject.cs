using System;
using System.ComponentModel;

namespace App_Code.Model
{
    public class FangsterObject
    {
        [Description("FangstId / int(11)")]
        public int FangstId { get; set; }

        [Description("Forfatter / varchar(50)")]
        public string Forfatter { get; set; }

        [Description("Dato / date")]
        public DateTime Dato { get; set; }

        [Description("Art / varchar(50)")]
        public string Art { get; set; }

        [Description("Vaegt / double")]
        public double Vaegt { get; set; }

        [Description("Laengde / double")]
        public double Laengde { get; set; }

        [Description("Fanger / varchar(10)")]
        public string Fanger { get; set; }

        [Description("Fiskevand  / varchar(50)")]
        public string Fiskevand { get; set; }

        [Description("Placering / varchar(100)")]
        public string Placering { get; set; }

        [Description("Land / varchar(30)")]
        public string Land { get; set; }

        [Description("Redskab  / varchar(100")]
        public string Redskab { get; set; }

        [Description("Vejr / varchar(250)")]
        public string Vejr { get; set; }

        [Description("Beskrivelse  / text")]
        public string Beskrivelse { get; set; }

        [Description("Link  / varchar(250)")]
        public string Link {get; set;}

        [Description("BilledMappe / varchar(100)")]
        public string BilledMappe { get; set; }

        [Description("Billed1 / varchar(50)")]
        public string Billed1 { get; set; }

        [Description("Billed2 / varchar(50)")]
        public string Billed2 { get; set; }

        [Description("Billed3 / varchar(50")]
        public string Billed3 { get; set; }

        [Description("Video1  / varchar(50)")]
        public string Video1 { get; set; }

        [Description("Video2 / varchar(50)")]
        public string Video2 { get; set; }

        [Description("Video3  / varchar(50)")]
        public string Video3 { get; set; }

        [Description("Offentlig  / smallint(6)")]
        public bool Offentlig { get; set; }

        [Description("YearFish  / smallint(6)")]
        public bool YearFish { get; set; }

        public FangsterObject(int fangstId, string forfatter, DateTime dato, string art, double vaegt, double laengde, string fanger, string fiskevand, string placering, string land, string redskab, string vejr, string beskrivelse, string link, string billedMappe, string billed1, string billed2, string billed3, string video1, string video2, string video3, bool offentlig, bool yearFish)
        {
            FangstId = fangstId;
            Forfatter = forfatter;
            Dato = dato;
            Art = art;
            Vaegt = vaegt;
            Laengde = laengde;
            Fanger = fanger;
            Fiskevand = fiskevand;
            Placering = placering;
            Land = land;
            Redskab = redskab;
            Vejr = vejr;
            Beskrivelse = beskrivelse;
            Link = link;
            BilledMappe = billedMappe;
            Billed1 = billed1;
            Billed2 = billed2;
            Billed3 = billed3;
            Video1 = video1;
            Video2 = video2;
            Video3 = video3;
            Offentlig = offentlig;
            YearFish = yearFish;
        }
    }
}
