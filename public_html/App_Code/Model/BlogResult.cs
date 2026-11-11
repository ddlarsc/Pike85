using System;
using Google.GData.Client;

namespace Model
{
    public class BlogResult
    {        
        public DateTime Published { get; private set; }
        public AtomEntry AtomEntry { get; set; }

        public BlogResult(DateTime published, AtomEntry atomEntry)
        {
            Published = published;
            AtomEntry = atomEntry;
        }
    }
}