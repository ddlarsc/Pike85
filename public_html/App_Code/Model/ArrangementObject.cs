using System;

namespace App_Code.Model
{
    public class ArrangementObject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int SetupId {get; set;}
        public int Decline { get; private set; }
        public DateTime ArrivelDate { get; private set; }
        public string ArrivalOnDay { get; private set; }
        public DateTime DepartureDate { get; private set; }
        public string DepartureOnDay { get; private set; }
        public string Message { get; private set; }
        public string MessageAboutBoat { get; private set; }
        public string MessageAboutBed { get; private set; }

        public ArrangementObject(int id, string name, int setupId, int decline, DateTime arrivelDate, string arrivalOnDay, DateTime departureDate, string departureOnDay, string message, string messageAboutBoat, string messageAboutBed)
        {
            Id = id;
            Name = name;
            SetupId = setupId;
            Decline = decline;
            ArrivelDate = arrivelDate;
            ArrivalOnDay = arrivalOnDay;
            DepartureDate = departureDate;
            DepartureOnDay = departureOnDay;
            Message = message;
            MessageAboutBoat = messageAboutBoat;
            MessageAboutBed = messageAboutBed;
        }
    }
}