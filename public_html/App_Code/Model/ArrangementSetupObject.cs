using System;
using System.Collections.Generic;
using App_Code.Model;

namespace Model
{
    public class ArrangementSetupObject
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Header { get; set; }
        public string Location { get; set; }
        public string Remarks { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<ArrangementObject> Arrangements { get; set; }
        public string Signature { get; set; }
        public DateTime TimeStamp { get; set; }

        public ArrangementSetupObject()
        {
            //Empty
        }

        public ArrangementSetupObject(int id, string user, string header, string location, string remarks, DateTime deadline, DateTime start, DateTime end, List<ArrangementObject> arrangements, string signature, DateTime timeStamp)
        {
            Id = id;
            User = user;
            Header = header;
            Location = location;
            Remarks = remarks;
            Deadline = deadline;
            Start = start;
            End = end;
            Arrangements = arrangements;
            Signature = signature;
            TimeStamp = timeStamp;
        }
    }
}