namespace Maid.Manga.DB
{
	using Microsoft.EntityFrameworkCore;
	using System;

	public class MangaDbContext : DbContext, IMangaDbContext
	{
		public MangaDbContext(DbContextOptions<MangaDbContext> options)
			: base(options) { }

		public DbSet<MangaInfo> MangaInfo { get; set; }

		public DbSet<MangaChapterInfo> MangaChapterInfo { get; set; }

		public DbSet<MangaSource> MangaSource { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<MangaSource>().HasData(
				new MangaSource {
					Id = Guid.NewGuid(),
					Name = "FanFox"
				}
			);
		}
	}
}
