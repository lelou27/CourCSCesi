using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyHN.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _bus;

        public CommentsController(IMediator bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Créer un commentaire sur un lien
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost()]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateComment(Guid id, CommentLinkCommand command)
        {
            var commentId = await _bus.Send(command);

            return Created($"comments/{commentId}", null);
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                UserId = User.Identity.Name,
                Authenticated = User.Identity.IsAuthenticated
            });
        }
    }
}
