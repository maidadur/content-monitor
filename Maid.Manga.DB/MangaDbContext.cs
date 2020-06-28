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
			modelBuilder.Entity<MangaInfo>()
				.HasMany(i => i.Chapters)
				.WithOne(i => i.Manga)
				.HasForeignKey(i => i.MangaId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
