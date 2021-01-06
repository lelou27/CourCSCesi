using MyHN.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class ShowLinkViewModel
    {
        public LinkDto Link { get; set; }
        public CommentsDto[] Comments { get; set; }

        public ShowLinkViewModel(LinkDto link, CommentsDto[] comments)
        {
            Link = link;
            Comments = comments;
        }
    }
}
