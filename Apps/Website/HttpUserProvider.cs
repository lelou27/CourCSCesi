using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyHN.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website
{
    public class HttpUserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public HttpUserProvider(IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
        {
            _httpContextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public string GetCurrentUserId()
        {
            return _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
        }
    }
}
