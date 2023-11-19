using IdentityServer4.Services;
using Maid.Auth.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Maid.Auth.API.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ILogger<AuthController> _log;
		private readonly UserManager<AppUser> _userManager;
		private readonly IIdentityServerInteractionService _interaction;

		public AuthController(SignInManager<AppUser> signInManager,
				UserManager<AppUser> userManager,
				IIdentityServerInteractionService interaction,
				ILogger<AuthController> logger) {
			_userManager = userManager;
			_interaction = interaction;
			_signInManager = signInManager;
			_log = logger;
		}

		/// <summary>
		/// Handle postback from username/password login
		/// </summary>
		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Route("login")]
		public async Task<IActionResult> Login(LoginModel model) {
			if (ModelState.IsValid) {
				_log.LogInformation("Logging user: " + model.Email);
				var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
				var user = await _userManager.FindByNameAsync(model.Email);
				if (user == null) {
					_log.LogError("User not found", user.Email);
					return NotFound();
				}
				if (context == null) {
					_log.LogError("Auth context is null");
					return StatusCode(500);
				}
				if (await _userManager.CheckPasswordAsync(user, model.Password)) {
					AuthenticationProperties props = new AuthenticationProperties {
						IsPersistent = true,
						ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12)
					};
					// TODO redo login
					//await HttpContext.SignInAsync(user.Id, user.UserName);
					return new JsonResult(new { RedirectUrl = model.ReturnUrl, IsOk = true });
				}
				_log.LogError("Password is incorrect");
			}
			return Unauthorized();
		}

		[HttpPost]
		public async Task<IActionResult> Logout(string logoutId) {
			await _signInManager.SignOutAsync();
			var context = await _interaction.GetLogoutContextAsync(logoutId);
			return Redirect(context.PostLogoutRedirectUri);
		}
	}
}