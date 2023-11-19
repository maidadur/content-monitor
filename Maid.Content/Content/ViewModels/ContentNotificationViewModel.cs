using Maid.Core;

namespace Maid.Content.ViewModels
{
	public class ContentNotificationViewModel : BaseEntity
	{
		public string Name { get; set; }

		public string Date { get; set; }

		public string Href { get; set; }

		public string ContentName { get; set; }

		public string ImageUrl { get; set; }

		public bool IsRead { get; set; }
	}
}