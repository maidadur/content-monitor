using System.Threading.Tasks;
using Maid.Manga.DB;

namespace Maid.Manga
{
	public interface IMangaLoader
	{
		Task<MangaInfo> LoadMangaInfoAsync(MangaInfo mangaInfo);
	}
}