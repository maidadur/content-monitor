using Microsoft.AspNetCore.Identity;

namespace Maid.Auth.API
{
	public class AppUser : IdentityUser
	{
		public string Name { get; set; }
	}
}