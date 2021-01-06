using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Vote
    {
        public Guid Id { get; }
        public VoteType Direction { get; }
        public DateTime CreatedAt { get; }

        internal Vote(VoteType direction)
        {
            this.Id = Guid.NewGuid();
            this.Direction = direction;
            this.CreatedAt = DateTime.UtcNow;
        }
    }
}
