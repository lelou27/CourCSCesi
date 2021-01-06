using Domain;
using MediatR;
using MyHN.Application;
using MyHN.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.VoteForLink
{
    public class VoteForLinkCommandHandler : IRequestHandler<VoteForLinkCommand>
    {
        private readonly ILinkRepository _repository;

        public VoteForLinkCommandHandler(ILinkRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(VoteForLinkCommand request, CancellationToken cancellationToken)
        {
            var link = _repository.GetById(request.LinkId);

            switch(request.Direction)
            {
                case VoteType.Up:
                    link.UpVote();
                    break;
                case VoteType.Down:
                    link.DownVote();
                    break;
            }

            await _repository.UpdateAsync(link);

            // TODO :: Mettre à jour le link via le repo

            return Unit.Value;
        }
    }
}
