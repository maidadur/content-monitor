using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Extensions;
using Maid.Auth.API.Consts;
using Maid.Auth.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Maid.Auth.API.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly IIdentityServerInteractionService _interaction;
		private readonly IClientStore _clientStore;
		private readonly IEventService _events;

		public AuthController(SignInManager<AppUser> signInManager, 
				UserManager<AppUser> userManager, 
				IIdentityServerInteractionService interaction, 
				IClientStore clientStore, 
				IEventService events) {
			_userManager = userManager;
			_interaction = interaction;
			_clientStore = clientStore;
			_events = events;
			_signInManager = signInManager;
		}

		/// <summary>
		/// Handle postback from username/password login
		/// </summary>
		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Route("login")]
		public async Task<IActionResult> Login(LoginModel model) {
			if (ModelState.IsValid) {
				var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
				var user = await _userManager.FindByNameAsync(model.Email);
				if (user != null && context != null && await _userManager.CheckPasswordAsync(user, model.Password)) {
					AuthenticationProperties props = new AuthenticationProperties {
						IsPersistent = true,
						ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12)
					};
					await HttpContext.SignInAsync(user.Id, user.UserName);
					return new JsonResult(new { RedirectUrl = model.ReturnUrl, IsOk = true });
				}
			}
			return Unauthorized();
		}


		[HttpPost]
		[Route("--reg--")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestViewModel model) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			var user = new AppUser { UserName = model.Email, Name = model.Name, Email = model.Email };
			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded) return BadRequest(result.Errors);
			await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
			await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("name", user.Name));
			await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
			await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", Roles.Consumer));
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> Logout(string logoutId) {
			await _signInManager.SignOutAsync();
			var context = await _interaction.GetLogoutContextAsync(logoutId);
			return Redirect(context.PostLogoutRedirectUri);
		}
	}
}
