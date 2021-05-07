using System.ComponentModel.DataAnnotations;

namespace Maid.Auth.API.Controllers
{
	public class RegisterRequestViewModel
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Password { get; set; }
	}
}