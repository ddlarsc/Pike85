using System;

namespace App_Code.Model
{
    public class NyhedObject
    {
        public int Id {get; private set;}
        public string Forfatter {get; private set;}
        public DateTime Dato {get; private set;}
        public string Overskrift {get; private set;}
        public string Beskrivelse {get; private set;}
        public string Link {get; private set;}
        public string BilledMappe {get; private set;}
        public string Billed1 {get; private set;}
        public string Billed2 {get; private set;}
        public string Billed3 {get; private set;}
        public string Video1 {get; private set;}
        public string Video2 {get; private set;}
        public string Video3 {get; private set;}
        public bool Offentlig {get; private set;}

        public NyhedObject(int id, string forfatter, DateTime dato, string overskrift, string beskrivelse, string link, string billedMappe, string billed1, string billed2, string billed3, string video1, string video2, string video3, bool offentlig)
        {
            Id = id;
            Forfatter = forfatter;
            Dato = dato;
            Overskrift = overskrift;
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
        }
    }
}