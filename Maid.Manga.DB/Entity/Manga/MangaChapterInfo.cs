namespace Maid.Manga.DB
{
	using Maid.Core;
	using System;
	using System.ComponentModel.DataAnnotations;

	public class MangaChapterInfo : BaseEntity
	{
		public string Name { get; set; }

		public string Date { get; set; }

		public string Href { get; set; }

		[Required]
		public MangaInfo Manga { get; set; }

		public Guid MangaId { get; set; }
	}
}