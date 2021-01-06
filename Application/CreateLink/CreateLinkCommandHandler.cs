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

        public CreateLinkCommandHandler(ILinkRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
            var link = new Link(request.Url);

            await _repository.AddAsync(link);

            return link.Id;
        }
    }
}
