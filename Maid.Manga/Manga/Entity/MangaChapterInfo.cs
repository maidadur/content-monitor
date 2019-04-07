namespace Maid.Manga
{
	using Maid.Core;

	public class MangaChapterInfo : BaseEntity
	{
		public string Name { get; set; }

		public string Date { get; set; }

		public string Href { get; set; }

		public MangaInfo Manga { get; set; }
	}
}
