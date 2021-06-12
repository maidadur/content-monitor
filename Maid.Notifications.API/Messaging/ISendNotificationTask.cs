using System.Threading.Tasks;

namespace Maid.Notifications.Api
{
	public interface ISendNotificationTask
	{
		Task SendNotificationFromTask(byte[] data);
	}
}