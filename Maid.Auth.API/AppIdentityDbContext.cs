using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Const = Maid.Auth.API.Consts;

namespace Maid.Auth.API
{
	public class AppIdentityDbContext : IdentityDbContext<AppUser>
	{
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Const.Roles.Consumer, NormalizedName = Const.Roles.Consumer.ToUpper() });
		}
	}
}