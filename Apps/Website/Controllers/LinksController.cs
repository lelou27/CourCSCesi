using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyHn.Application;
using MyHN.Application;
using System;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Controllers
{
    public class LinksController : BaseController
    {
        private IMediator _bus;

        public LinksController(IMediator bus)
        {
            _bus = bus;
        }

        public IActionResult Create()
        {
            return View(new CreateLinkCommand());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLinkCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            Guid createdLinkId = await _bus.Send(command);

            this.Success("Le lien a été correctement publié !");

            return RedirectToAction(nameof(Index));

        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var linkDtos = await _bus.Send(new GetLinksQuery());
            
            return View(linkDtos);
        }

        [AllowAnonymous]
        [HttpGet("/{controller}/{id:guid}")]
        public async Task<IActionResult> Show(Guid id)
        {
            var linkDto = await _bus.Send(new GetLinkByIdQuery(id));
            var modelDto = await _bus.Send(new GetCommentsByLinkQuery(id));

            return View(new ShowLinkViewModel(linkDto, modelDto));
        }

        [HttpPost("/{controller}/{id:guid}/vote")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vote(Guid id, VoteType direction, string redirectTo)
        {
            await _bus.Send(new VoteForLinkCommand() {
                LinkId = id,
                Direction = direction
            });

            this.Success("Votre vote a été pris en compte");

            return Redirect(redirectTo);
        }

        [HttpGet("{controller}/{id:guid}/comment")]
        public async Task<IActionResult> Comment(Guid id)
        {
            return await ShowCommentForm(id, new CommentLinkCommand(id));
        }

        private async Task<IActionResult> ShowCommentForm(Guid linkId, CommentLinkCommand command)
        {
            var link = await _bus.Send(new GetLinkByIdQuery(linkId));
            return View(new CommentFormViewModel(link, command));
        }

        [HttpPost("/{controller}/{id:guid}/comment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(Guid id, CommentLinkCommand command)
        {
            if(!ModelState.IsValid)
            {
                return await ShowCommentForm(id, command);
            }

            await _bus.Send(command);
            return RedirectToAction(nameof(Show), new { Id = id });
        }

        //[TypeFilter(typeof(CustomExceptionFilter))]
        public IActionResult Error()
        {
            throw new NotImplementedException("Impossible d'accéder à cette page !");
        }
    }
}
