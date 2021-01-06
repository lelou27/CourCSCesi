using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyHN.Application
{
    public class CreateLinkCommand: IRequest<Guid>
    {
        [Required]
        [Url]
        public string Url { get; set; }
    }
}
