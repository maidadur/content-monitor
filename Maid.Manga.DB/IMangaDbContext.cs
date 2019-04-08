using Microsoft.EntityFrameworkCore;

namespace Maid.Manga.DB
{
	public interface IMangaDbContext
	{
		DbSet<MangaInfo> MangaInfo { get; set; }

		DbSet<MangaChapterInfo> MangaChapterInfo { get; set; }
	}
}