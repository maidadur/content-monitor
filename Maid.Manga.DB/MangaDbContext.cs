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
					Id = new Guid("ADFB04FE-231C-41ED-8623-5EA3BB50C400"),
					CreatedOn = DateTime.Now,
					Name = "FanFox"
				}
			);
		}
	}
}
