using Maid.Notifications.Api.Entities;
using Maid.Notifications.DB;
using System.Threading.Tasks;

namespace Maid.Notifications.Api.NotificationClients
{
	public interface INotificationClient
	{
		Task SendNotificationsAsync(Subscription subscription, Notification notification);
	}
}
