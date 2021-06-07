namespace Maid.Manga
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using Maid.Notifications;
	using Maid.RabbitMQ;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class MangaLoadTask
	{
		private readonly IMessageClient _messageClient;
		private IEntityRepository<MangaChapterInfo> _chaptersRep;
		private ILogger<MangaLoadTask> _log;
		private IEntityRepository<MangaInfo> _mangaInfoRep;
		private IMangaLoader _mangaLoader;
		private IEntityRepository<MangaChapterNotification> _mangaNotificationRep;

		public MangaLoadTask(
				IEntityRepository<MangaInfo> mangaInfoRep,
				IEntityRepository<MangaChapterInfo> chaptersRep,
				IEntityRepository<MangaChapterNotification> mangaNotificationRep,
				IMangaLoader mangaLoader,
				IMessageClient messageClient,
				ILogger<MangaLoadTask> log) {
			_mangaInfoRep = mangaInfoRep;
			_mangaLoader = mangaLoader;
			this._messageClient = messageClient;
			_chaptersRep = chaptersRep;
			_mangaNotificationRep = mangaNotificationRep;
			_log = log;
		}

		private void CreateNewMangaNotifications(List<MangaChapterInfo> newChapters) {
			var newChapterNotification = new MangaChapterNotification {
				MangaChapterInfo = newChapters.Last()
			};
			if (newChapters.Count > 1) {
				newChapterNotification.MangaChapterInfo.Name += " - " + newChapters.First().Name;
			}
			_mangaNotificationRep.Create(newChapterNotification);
			_mangaNotificationRep.Save();
			SendNotificationMessage(newChapterNotification);
		}

		private void SendNotificationMessage(MangaChapterNotification newChapterNotification) {
			var notification = new Notification {
				Title = newChapterNotification.MangaChapterInfo.Manga.Name,
				Body = newChapterNotification.MangaChapterInfo.Name,
				Icon = newChapterNotification.MangaChapterInfo.Manga.ImageUrl
			};
			_messageClient.SendMessage("notifications", notification);
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