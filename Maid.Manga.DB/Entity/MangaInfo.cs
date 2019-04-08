namespace Maid.Manga.DB
{
	using Maid.Core;
	using System.Collections.Generic;

	public class MangaInfo : BaseEntity
	{
		public string Name { get; set; }

		public string ImageUrl { get; set; }

		public string Href { get; set; }

		public virtual List<MangaChapterInfo> Chapters { get; set; }
	}
}
