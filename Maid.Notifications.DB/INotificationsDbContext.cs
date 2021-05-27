namespace Maid.Notifications.DB
{
	using Microsoft.EntityFrameworkCore;

	public interface INotificationsDbContext
	{
		DbSet<Subscription> Subscriptions { get; set; }

	}
}