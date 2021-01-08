using MediatR;
using MyHN.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyHN.Application
{
    class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, Guid>
    {
        private readonly ILinkRepository _repository;
        private readonly IUserProvider _userProvider;

        public CreateLinkCommandHandler(ILinkRepository repository, IUserProvider userProvider)
        {
            _repository = repository;
            _userProvider = userProvider;
        }

        public async Task<Guid> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
            var link = new Link(request.Url, _userProvider.GetCurrentUserId());

            await _repository.AddAsync(link);

            return link.Id;
        }
    }
}
