namespace Maid.Notifications.DB
{
	using Microsoft.EntityFrameworkCore;

	public class NotificationsDbContext : DbContext, INotificationsDbContext
	{
		public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
			: base(options) { }

		public DbSet<Subscription> Subscriptions { get; set; }
	}
}
