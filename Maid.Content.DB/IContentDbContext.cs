using Microsoft.EntityFrameworkCore;

namespace Maid.Content.DB
{
	public interface IContentDbContext
	{
		DbSet<ContentInfo> ContentInfo { get; set; }

		DbSet<ContentItemInfo> ContentItemInfo { get; set; }
	}
}