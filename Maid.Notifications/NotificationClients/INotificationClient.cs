using Maid.Notifications.DB;
using System.Threading.Tasks;

namespace Maid.Notifications
{
	public interface INotificationClient
	{
		Task SendNotificationsAsync(Subscription subscription, Notification notification);
	}
}
