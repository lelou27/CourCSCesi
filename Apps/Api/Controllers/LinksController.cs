using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyHn.Application;
using MyHN.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/links")]
    public class LinksController : ControllerBase
    {
        private readonly IMediator _bus;

        public LinksController(IMediator bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Récupère les liens.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<LinkDto[]> GetLinks()
        {
            return await _bus.Send(new GetLinksQuery());
        }

        /// <summary>
        /// Récupère un lien.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(LinkDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<LinkDto> GetLinkById(Guid id)
        {
            return await _bus.Send(new GetLinkByIdQuery(id));
        }

        /// <summary>
        /// Récupère les commentaires des liens.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}/comments")]
        public async Task<CommentsDto[]> GetLinksComments(Guid id)
        {
            return await _bus.Send(new GetCommentsByLinkQuery(id));
        }

        /// <summary>
        /// Met un pouce en l'air.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}/upVote")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpVote(Guid id)
        {
            await _bus.Send(new VoteForLinkCommand
            {
                LinkId = id,
                Direction = Domain.VoteType.Up
            });
            return NoContent();
        }

        /// <summary>
        /// Met un pouce en bas.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}/downVote")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DownVote(Guid id)
        {
            await _bus.Send(new VoteForLinkCommand
            {
                LinkId = id,
                Direction = Domain.VoteType.Down
            });
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateLink(CreateLinkCommand command)
        {
            var linkId = await _bus.Send(command);

            return CreatedAtAction(nameof(GetLinkById), new { id = linkId }, null);
        }
    }
}
