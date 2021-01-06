using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyHN.Application
{
    class GetCommentsByLinkQueryHandler : IRequestHandler<GetCommentsByLinkQuery, CommentsDto[]>
    {
        private readonly IContext _context;

        public GetCommentsByLinkQueryHandler(IContext context)
        {
            _context = context;
        }

        public Task<CommentsDto[]> Handle(GetCommentsByLinkQuery request, CancellationToken cancellationToken)
        {
            var results = from comment in _context.Comments
                          where comment.LinkId == request.LinkId
                          orderby comment.CreatedAt descending
                          select new CommentsDto
                          {
                              Content = comment.Content
                          };

            return Task.FromResult(results.ToArray());
        }
    }
}
