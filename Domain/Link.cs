using Domain;
using System;
using System.Collections.Generic;

namespace MyHN.Domain
{
    /// <summary>
    /// Représente l'entité lien
    /// </summary>
    public class Link
    {
        public Guid Id { get; private set; }
        public string Url { get; }
        public DateTime CreatedAt { get; }
        public string CreatedBy { get; }

        private List<Vote> _votes;

        public IReadOnlyList<Vote> Votes => _votes;
        
        public Link(string url, string createdBy)
        {
            this.Id = Guid.NewGuid();
            this.Url = url;
            this.CreatedAt = DateTime.UtcNow;
            this.CreatedBy = createdBy;
            this._votes = new List<Vote>();
        }

        public void UpVote()
        {
            this._votes.Add(new Vote(VoteType.Up));
        }

        public void DownVote()
        {
            this._votes.Add(new Vote(VoteType.Down));
        }

        public Comment AddComment(string content)
        {
            return new Comment(Id, content);
        }
    }
}
