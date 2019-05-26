namespace Maid.Manga.Manga
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using System.Linq;
	using System.Threading.Tasks;

	public class MangaLoadTask
	{
		private IMangaLoader _mangaLoader;
		private IEntityRepository<MangaChapterInfo> _chaptersRep;
		private IEntityRepository<MangaInfo> _mangaInfoRep;

		public MangaLoadTask(
				IEntityRepository<MangaInfo> mangaInfoRep,
				IEntityRepository<MangaChapterInfo> chaptersRep,
				IMangaLoader mangaLoader) {
			_mangaInfoRep = mangaInfoRep;
			_mangaLoader = mangaLoader;
			_chaptersRep = chaptersRep;
		}

		private async Task UpdateMangaChapters(MangaInfo manga) {
			manga = await _mangaLoader.LoadMangaInfoAsync(manga);
			var chapters = _chaptersRep.GetByAsync(chapter => chapter.MangaId == manga.Id);
			var currentChapters = manga.Chapters;
			bool hasNew = false;
			currentChapters.ForEach(chapter => {
				if (currentChapters.Any(c => c.Name == chapter.Name)) {
					return;
				}
				hasNew = true;
				_chaptersRep.Create(chapter);
			});
			if (hasNew) {
				await _chaptersRep.SaveAsync();
			}
		}

		public async Task LoadMangaInfos() {
			var mangas = await _mangaInfoRep.GetAllAsync();
			mangas.ForEach(async (manga) => {
				await UpdateMangaChapters(manga);
			});
		}

	}
}
