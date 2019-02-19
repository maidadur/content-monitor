namespace MyHouseMaid.Models
{
	using System;

	public class MangaChapter : BaseEntity
	{
		public string Name { get; set; }
		public string Href { get; set; }
		public DateTime Date { get; set; }
		public Manga Manga { get; set; }
	}
}
