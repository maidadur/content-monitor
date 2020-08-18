namespace Maid.Manga
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class MangaLoadTask
	{
		private IMangaLoader _mangaLoader;
		private IEntityRepository<MangaChapterInfo> _chaptersRep;
		private IEntityRepository<MangaInfo> _mangaInfoRep;
		private IEntityRepository<MangaChapterNotification> _mangaNotificationRep;

		public MangaLoadTask(
				IEntityRepository<MangaInfo> mangaInfoRep,
				IEntityRepository<MangaChapterInfo> chaptersRep,
				IEntityRepository<MangaChapterNotification> mangaNotificationRep,
				IMangaLoader mangaLoader) {
			_mangaInfoRep = mangaInfoRep;
			_mangaLoader = mangaLoader;
			_chaptersRep = chaptersRep;
			_mangaNotificationRep = mangaNotificationRep;
		}

		private async Task UpdateMangaChaptersAsync(MangaInfo manga) {
			var newManga = await _mangaLoader.LoadMangaInfoAsync(manga);
			var chapters = await _chaptersRep.GetByAsync(chapter => chapter.MangaId == manga.Id);
			var currentChapters = manga.Chapters;
			var newChapters = new List<MangaChapterInfo>();
			newManga.Chapters.ForEach(chapter => {
				if (currentChapters.Any(c => c.Name == chapter.Name)) {
					return;
				}
				newChapters.Add(chapter);
				_chaptersRep.Create(chapter);
			});
			if (newChapters.IsNotEmpty()) {
				_chaptersRep.Save();
				CreateNewMangaNotifications(newChapters);
			}
		}

		//TODO create notifications class
		private void CreateNewMangaNotifications(List<MangaChapterInfo> newChapters) {
			newChapters.ForEach(chapter => _mangaNotificationRep.Create(new MangaChapterNotification() {
				MangaChapterInfo = chapter
			}));
			_mangaNotificationRep.Save();
		}

		public async Task LoadMangaInfosAsync() {
			var mangas = await _mangaInfoRep.GetAllAsync();
			await mangas.ForEachAsync(async (manga) => {
				await UpdateMangaChaptersAsync(manga);
			});
		}

	}
}
