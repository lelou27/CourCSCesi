using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHN.Domain
{
    public class Comment
    {
        public Guid Id { get; }
        public Guid LinkId { get; }
        public string Content { get; }
        public DateTime CreatedAt { get; }

        internal Comment(Guid LinkId, string content)
        {
            this.Id = Guid.NewGuid();
            this.LinkId = LinkId;
            this.Content = content;
            this.CreatedAt = DateTime.UtcNow;
        }
    }
}
