namespace Maid.Manga.DB
{
	using Microsoft.EntityFrameworkCore;
	using System;

	public class MangaDbContext : DbContext
	{
		public MangaDbContext(DbContextOptions<MangaDbContext> options)
			: base(options) { }

		public DbSet<MangaInfo> MangaInfo { get; set; }

		public DbSet<MangaChapterInfo> MangaChapterInfo { get; set; }

		public DbSet<MangaSource> MangaSource { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<MangaSource>().HasData(
				new MangaSource {
					Id = new Guid("CB5CEA99-FF7E-4272-0C11-08D6C115FE81"),
					CreatedOn = DateTime.Now,
					Name = "FanFox",
					DomainUrl = "http://fanfox.net"
				},
				new MangaSource {
					Id = new Guid("DB5CEA99-FF7E-4272-0C11-08D6C115FE81"),
					CreatedOn = DateTime.Now,
					Name = "MangaRock",
					DomainUrl = "https://mangarock.com"
				}
			);
			modelBuilder.Entity<MangaInfo>()
				.HasMany(i => i.Chapters)
				.WithOne(i => i.Manga)
				.HasForeignKey(i => i.MangaId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
