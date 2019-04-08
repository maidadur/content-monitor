namespace Maid.Manga.DB
{
	using Microsoft.EntityFrameworkCore;

	public class MangaDbContext : DbContext, IMangaDbContext
	{
		public MangaDbContext(DbContextOptions<MangaDbContext> options)
			: base(options) { }

		public DbSet<MangaInfo> MangaInfo { get; set; }

		public DbSet<MangaChapterInfo> MangaChapterInfo { get; set; }
	}
}
