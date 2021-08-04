﻿namespace Maid.Manga
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using Maid.Notifications;
	using Maid.RabbitMQ;
	using Microsoft.Extensions.Logging;
	using System;
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
			var newNotifications = new List<MangaChapterNotification>();
			newChapters.ForEach(chapter => {
				var newNotification = new MangaChapterNotification() {
					MangaChapterInfo = chapter
				};
				newNotifications.Add(newNotification);
				_mangaNotificationRep.Create(newNotification);
			});
			_mangaNotificationRep.Save();
			SendNotificationMessage(newNotifications.Last());
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
			_log.LogInformation($"Started loading new chapters for '{manga.Name}' title");
			try {
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
			} catch (Exception ex) {
				_log.LogError($"Error while loading new info for '{manga.Name}' title. Error info: \n{ex.Message}\n{ex.StackTrace}");
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