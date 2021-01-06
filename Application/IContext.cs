using MyHN.Domain;
using System.Linq;

namespace MyHN.Application
{
    public interface IContext
    {
        IQueryable<Link> Links { get; }
        IQueryable<Comment> Comments { get; }
    }
}
