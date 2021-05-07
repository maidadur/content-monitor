using System.ComponentModel.DataAnnotations;

namespace Maid.Auth.API.Models
{
	public class LoginModel
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public string ReturnUrl { get; set; }
	}
}