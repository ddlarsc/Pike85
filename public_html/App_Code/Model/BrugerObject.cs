using System;
using System.ComponentModel;

namespace App_Code.Model
{
    public class BrugerObject
    {
        [Description("BrugerId / varchar(20)")]
        public string UserId { get; set; }

        [Description("Kodeord / varchar(50)")]
        public string UserCode { get; set; }

        [Description("Navn / varchar(50)")]
        public string Name { get; set; }

        [Description("KortNavn / varchar(20)")]
        public string ShortName { get; set; }

        [Description("Foedselsdag / date")]
        public DateTime Birthday { get; set; }

        [Description("Beskrivelse / text")]
        public string Description { get; set; }

        [Description("Email / varchar(100)")]
        public string Email1 { get; set; }

        [Description("Billed / varchar(20)")]
        public string Image { get; set; }

        [Description("Roller / varchar(250)")]
        public string UserLevel { get; set; }

        [Description("Offentlig / tinyint(4)")]
        public bool Public { get; set; }

        [Description("Adresse / varchar(50)")]
        public string Street { get; set; }

        [Description("By / varchar(30)")]
        public string City { get; set; }

        [Description("PostNr / varchar(4)")]
        public string ZipCode { get; set; }

        [Description("Email2 / varchar(50")]
        public string Email2 { get; set; }

        [Description("HjemTlf / varchar(30)")]
        public string PhonePrivate { get; set; }

        [Description("ArbTlf / varchar(30)")]
        public string PhoneWork { get; set; }

        [Description("MobilTlf / varchar(30)")]
        public string PhoneMobil { get; set; }

        [Description("Indmeldt / date")]
        public DateTime Enrolled { get; set; }

        [Description("Dead / tinyint(1)")]
        public bool Dead { get; set; }

        public BrugerObject(string userId, string userCode)
        {
            UserId = userId;
            UserCode = userCode;
        }

        public BrugerObject(string userId, string userCode, string name, string shortName, DateTime birthday, string description, string email1, string image, string userLevel, bool @public, string street, string city, string zipCode, string email2, string phonePrivate, string phoneWork, string phoneMobil, DateTime enrolled, bool dead)
        {
            UserId = userId;
            UserCode = userCode;
            Name = name;
            ShortName = shortName;
            Birthday = birthday;
            Description = description;
            Email1 = email1;
            Image = image;
            UserLevel = userLevel;
            Public = @public;
            Street = street;
            City = city;
            ZipCode = zipCode;
            Email2 = email2;
            PhonePrivate = phonePrivate;
            PhoneWork = phoneWork;
            PhoneMobil = phoneMobil;
            Enrolled = enrolled;
            Dead = dead;
        }
    }
}