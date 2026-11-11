using System;

namespace Model
{
    public class BlogAtomicEntryContent
    {
        public DateTime Published { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }

        public BlogAtomicEntryContent(DateTime published, string title, string content, string type)
        {
            Published = published;
            Title = title;
            Content = content;
            Type = type;
        }
    }
}