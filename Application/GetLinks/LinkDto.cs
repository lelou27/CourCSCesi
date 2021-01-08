using System;

namespace MyHN.Application
{
    public class LinkDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByName { get; set; }
        public int UpVotesCount { get; set; }
        public int DownVotesCount { get; set; }
    }
}
