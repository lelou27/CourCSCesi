using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenOptions _options;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _factory;

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenOptions options,
            IUserClaimsPrincipalFactory<IdentityUser> factory) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _options = options;
            _factory = factory;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Register(RegisterViewModel request)
        {
            var result = await _userManager.CreateAsync(new IdentityUser(request.Username), request.Password);

            if(!result.Succeeded)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if(user == null)
            {
                return BadRequest();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if(!result.Succeeded)
            {
                return BadRequest();
            }

            var principal = await _factory.CreateAsync(user);
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = (ClaimsIdentity)principal.Identity,
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                SigningCredentials = new SigningCredentials(_options.Key, SecurityAlgorithms.HmacSha256Signature),
            };

            return Ok(new JwtSecurityTokenHandler().CreateEncodedJwt(tokenDescriptior));
        }
    }
}
