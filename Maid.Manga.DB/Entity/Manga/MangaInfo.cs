namespace Maid.Manga.DB
{
	using Maid.Core;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;

	public class MangaInfo : BaseEntity
	{
		public string Name { get; set; }

		public string ImageUrl { get; set; }

		public string Href { get; set; }

		public MangaSource Source { get; set; }

		public List<MangaChapterInfo> Chapters { get; set; }
	}
}
