using MediatR;
using System;

namespace MyHN.Application
{
    public class GetLinkByIdQuery : IRequest<LinkDto>
    {
        public Guid Id { get; set; }

        public GetLinkByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
