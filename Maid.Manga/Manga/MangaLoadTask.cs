namespace Maid.Manga
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class MangaLoadTask
	{
		private IMangaLoader _mangaLoader;
		private IEntityRepository<MangaChapterInfo> _chaptersRep;
		private IEntityRepository<MangaInfo> _mangaInfoRep;
		private IEntityRepository<MangaChapterNotification> _mangaNotificationRep;
		private ILogger<MangaLoadTask> _log;

		public MangaLoadTask(
				IEntityRepository<MangaInfo> mangaInfoRep,
				IEntityRepository<MangaChapterInfo> chaptersRep,
				IEntityRepository<MangaChapterNotification> mangaNotificationRep,
				IMangaLoader mangaLoader,
				ILogger<MangaLoadTask> log) {
			_mangaInfoRep = mangaInfoRep;
			_mangaLoader = mangaLoader;
			_chaptersRep = chaptersRep;
			_mangaNotificationRep = mangaNotificationRep;
			_log = log;
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
			_log.LogInformation($"Found {newChapters.Count} new chapters for '{newManga.Name}' title");
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
			_log.LogInformation("LoadMangaInfosAsync. Start loading manga");
			var mangas = await _mangaInfoRep.GetAllAsync();
			await mangas.ForEachAsync(async (manga) => {
				await UpdateMangaChaptersAsync(manga);
			});
			_log.LogInformation("LoadMangaInfosAsync. Ended loading manga");
		}
	}
}