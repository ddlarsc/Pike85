using System;
using System.ComponentModel;

namespace App_Code.Model
{
    public class OpslagObject
    {
        [Description("OpslagId / int(11)")]
        public int OpslagId {get; set;}

        [Description("Forfatter / varchar(50)")]
        public string Forfatter {get; set;}

        [Description("Dato / Date")]
        public DateTime Dato {get; set;}

        [Description("Overskrift / varchar(100)")]
        public string Overskrift {get; set;}

        [Description("Beskrivelse / Text")]
        public string Beskrivelse {get; set;}

        [Description("Link / varchar(250)")]
        public string Link {get; set;}

        [Description("BilledMappe / varchar(100)")]
        public string BilledMappe {get; set;}

        [Description("Billed / varchar(50)")]
        public string Billed {get; set;}

        [Description("Video1 / varchar(50)")]
        public string Video1 {get; set;}

        [Description("Offentlig / Tinyint(4)")]
        public bool Offentlig {get; set;}
    }
}