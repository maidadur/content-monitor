using Maid.Content.DB;
using System.Threading.Tasks;

namespace Maid.Content
{
	public interface IContentLoader
	{
		Task<ContentInfo> LoadContentInfoAsync(ContentInfo contentInfo);
	}
}