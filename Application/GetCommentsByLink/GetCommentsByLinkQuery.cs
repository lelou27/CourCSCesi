using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHN.Application
{
    public class GetCommentsByLinkQuery : IRequest<CommentsDto[]>
    {
        public Guid LinkId { get; set; }

        public GetCommentsByLinkQuery(Guid linkId)
        {
            LinkId = linkId;
        }
    }
}
