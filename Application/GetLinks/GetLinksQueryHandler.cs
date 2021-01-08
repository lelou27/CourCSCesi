using Domain;
using MediatR;
using MyHn.Application;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyHN.Application.GetLinks
{
    class GetLinksQueryHandler: IRequestHandler<GetLinksQuery, LinkDto[]>
    {
        private readonly IContext _context;

        public GetLinksQueryHandler(IContext context)
        {
            _context = context;
        }

        public Task<LinkDto[]> Handle(GetLinksQuery request, CancellationToken cancellationToken)
        {
            /*  return Task.FromResult(
                  _context.Links.Select(o => new LinkDto
                                                          { 
                                                              Id = o.Id,
                                                              Url = o.Url,
                                                              CreatedAt = o.CreatedAt,
                                                              CreatedByName = "",
                                                              UpVotesCount = o.Votes.Count(v => v.Direction == VoteType.Up),
                                                              DownVotesCount = o.Votes.Count(v => v.Direction == VoteType.Down),
                  }
                                          ).OrderByDescending(o => o.CreatedAt).ToArray());*/

            var results = from link in _context.Links
                          join user in _context.Users on link.CreatedBy equals user.Id
                          select new LinkDto
                          {
                              Id = link.Id,
                              Url = link.Url,
                              CreatedAt = link.CreatedAt,
                              CreatedByName = user.UserName,
                              UpVotesCount = link.Votes.Count(v => v.Direction == VoteType.Up),
                              DownVotesCount = link.Votes.Count(v => v.Direction == VoteType.Down),
                          };

            return Task.FromResult(results.OrderByDescending(o => o.CreatedAt).ToArray());
        }

    }
}
