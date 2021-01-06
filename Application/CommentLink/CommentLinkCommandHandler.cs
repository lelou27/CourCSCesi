using MediatR;
using MyHN.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyHN.Application
{
    public class CommentLinkCommandHandler : IRequestHandler<CommentLinkCommand, Guid>
    {
        private readonly ILinkRepository _linkRepo;
        private readonly ICommentRepository _commentRepo;

        public CommentLinkCommandHandler(ILinkRepository linkRepo, ICommentRepository commentRepo)
        {
            _linkRepo = linkRepo;
            _commentRepo = commentRepo;
        }

        public async Task<Guid> Handle(CommentLinkCommand request, CancellationToken cancellationToken)
        {
            var link = _linkRepo.GetById(request.LinkId);
            var comment = link.AddComment(request.Content);

            await _commentRepo.AddAsync(comment);

            return comment.Id;
        }

    }
}
