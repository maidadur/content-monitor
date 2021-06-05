using Maid.Core;

namespace Maid.Manga.ViewModels
{
	public class MangaChapterNotificationViewModel : BaseEntity
	{
		public string Name { get; set; }

		public string Date { get; set; }

		public string Href { get; set; }

		public string MangaName { get; set; }

		public string ImageUrl { get; set; }

		public bool IsRead { get; set; }
	}
}