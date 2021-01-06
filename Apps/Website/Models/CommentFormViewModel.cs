using MyHN.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class CommentFormViewModel
    {
        public LinkDto Link { get; set; }
        public CommentLinkCommand Comment { get; set; }

        public CommentFormViewModel(LinkDto link, CommentLinkCommand comment)
        {
            Link = link;
            Comment = comment;
        }
    }
}
