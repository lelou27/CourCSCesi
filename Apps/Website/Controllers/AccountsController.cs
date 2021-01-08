using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Controllers
{
    [AllowAnonymous]
    public class AccountsController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signingManager;

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signingManager = signInManager;
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var user = new IdentityUser(request.Username);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(RegisterViewModel.Username), "Une erreur est survenue");
                return View(request);
            }

            this.Success("Votre compte a été créé.");

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _signingManager.PasswordSignInAsync(request.Username, request.Password, true, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(RegisterViewModel.Username), "Une erreur est survenue");
                return View(request);
            }

            this.Success("Connexion réussie.");
            return RedirectToAction(nameof(LinksController.Index), "Links");
        }

        public async Task<IActionResult> Logout()
        {
            await _signingManager.SignOutAsync();

            this.Success("Vous êtes déconnecté.");

            return RedirectToAction(nameof(LinksController.Index), "Links");
        }
    }
}
