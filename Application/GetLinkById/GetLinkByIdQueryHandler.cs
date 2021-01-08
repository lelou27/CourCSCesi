using Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyHN.Application
{
    public class GetLinkByIdQueryHandler : IRequestHandler<GetLinkByIdQuery, LinkDto>
    {
        private readonly IContext _context;

        public GetLinkByIdQueryHandler(IContext context)
        {
            _context = context;
        }
        public Task<LinkDto> Handle(GetLinkByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Links.Where(o => o.Id == request.Id).Select(o => new LinkDto
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                Url = o.Url,
                UpVotesCount = o.Votes.Count(v => v.Direction == VoteType.Up),
                DownVotesCount = o.Votes.Count(v => v.Direction == VoteType.Down),
            }).Single());
        }
    }
}
