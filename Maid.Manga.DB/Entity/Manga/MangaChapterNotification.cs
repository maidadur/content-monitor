namespace Maid.Manga.DB
{
	using Maid.Core;
	using System;

	public class MangaChapterNotification : BaseNotification
	{
		public MangaChapterInfo MangaChapterInfo { get; set; }
		public Guid MangaChapterInfoId { get; set; }
	}
}
