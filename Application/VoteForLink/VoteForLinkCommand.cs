using Domain;
using MediatR;
using System;

namespace MyHN.Application
{
    public class VoteForLinkCommand : IRequest
    {
        public Guid LinkId { get; set; }
        public VoteType Direction { get; set; }
    }
}
