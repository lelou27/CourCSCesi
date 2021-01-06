using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHN.Application
{
    public class CommentLinkCommand: IRequest<Guid>
    {
        public Guid LinkId { get; set; }
        public string Content { get; set; }

        public CommentLinkCommand()
        {}

        public CommentLinkCommand(Guid LinkId)
        {
            this.LinkId = LinkId;
        }
    }
}
