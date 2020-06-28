using Maid.Core;

namespace Maid.Manga.DB
{
	public class MangaSource : BaseLookup
	{
		public string DomainUrl { get; set; }

		public string TitleXpath { get; set; }

		public string ImageXpath { get; set; }

		public string ChapterXpath { get; set; }

		public string ChapterHrefXpath { get; set; }

		public string ChapterTitleXpath { get; set; }

		public string ChapterDateXpath { get; set; }

		public string ImageUrl { get; set; }
	}
}
