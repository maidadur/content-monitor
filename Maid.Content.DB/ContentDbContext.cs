namespace Maid.Content.DB
{
	using Microsoft.EntityFrameworkCore;

	public class ContentDbContext : DbContext
	{
		public ContentDbContext(DbContextOptions<ContentDbContext> options)
			: base(options) { }

		public DbSet<ContentInfo> ContentInfo { get; set; }

		public DbSet<ContentItemInfo> ContentItemInfo { get; set; }

		public DbSet<ContentSource> ContentSource { get; set; }

		public DbSet<ContentItemNotification> ContentItemNotification { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<ContentInfo>()
				.HasMany(i => i.Items)
				.WithOne(i => i.ContentInfo)
				.HasForeignKey(i => i.ContentInfoId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}