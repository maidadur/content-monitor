using Maid.Manga.DB;
using System.Threading.Tasks;

namespace Maid.Manga
{
	public interface IMangaLoader
	{
		Task<MangaInfo> LoadMangaInfoAsync(MangaInfo mangaInfo);
	}
}