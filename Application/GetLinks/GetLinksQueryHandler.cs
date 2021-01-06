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
            return Task.FromResult(
                _context.Links.Select(o => new LinkDto
                                                        { 
                                                            Id = o.Id,
                                                            Url = o.Url,
                                                            CreatedAt = o.CreatedAt,
                                                            UpVotesCount = o.Votes.Count(v => v.Direction == VoteType.Up),
                                                            DownVotesCount = o.Votes.Count(v => v.Direction == VoteType.Down),
                }
                                        ).OrderByDescending(o => o.CreatedAt).ToArray());
        }

    }
}
