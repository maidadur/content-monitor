namespace Maid.Content
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Content.DB;
	using Maid.Notifications;
	using Maid.RabbitMQ;
	using Microsoft.Extensions.Logging;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class ContentLoadTask
	{
		private readonly IMessageClient _messageClient;
		private IEntityRepository<ContentItemInfo> _chaptersRep;
		private ILogger<ContentLoadTask> _log;
		private IEntityRepository<ContentInfo> _contentInfoRep;
		private IContentLoader _contentLoader;
		private IEntityRepository<ContentItemNotification> _contentNotificationRep;

		public ContentLoadTask(
				IEntityRepository<ContentInfo> contentInfoRep,
				IEntityRepository<ContentItemInfo> chaptersRep,
				IEntityRepository<ContentItemNotification> contentNotificationRep,
				IContentLoader contentLoader,
				IMessageClient messageClient,
				ILogger<ContentLoadTask> log) {
			_contentInfoRep = contentInfoRep;
			_contentLoader = contentLoader;
			_messageClient = messageClient;
			_chaptersRep = chaptersRep;
			_contentNotificationRep = contentNotificationRep;
			_log = log;
		}

		private void CreateNewContentNotifications(List<ContentItemInfo> newChapters) {
			var newNotifications = new List<ContentItemNotification>();
			newChapters.ForEach(chapter => {
				var newNotification = new ContentItemNotification() {
					ContentItemInfo = chapter
				};
				newNotifications.Add(newNotification);
				_contentNotificationRep.Create(newNotification);
			});
			_contentNotificationRep.Save();
			SendNotificationMessage(newNotifications.Last());
		}

		private void SendNotificationMessage(ContentItemNotification newChapterNotification) {
			var notification = new Notification {
				Title = newChapterNotification.ContentItemInfo.ContentInfo.Name,
				Body = newChapterNotification.ContentItemInfo.Name,
				Icon = newChapterNotification.ContentItemInfo.ContentInfo.ImageUrl
			};
			_messageClient.SendMessage("notifications", notification);
		}

		private void SendStatusNotification(ContentInfo newContent) {
			var notification = new Notification {
				Title = newContent.Name,
				Body = newContent.Status,
				Icon = newContent.ImageUrl
			};
			_messageClient.SendMessage("notifications", notification);
		}

		private async Task UpdateContentChaptersAsync(ContentInfo contentInfo) {
			_log.LogInformation($"Started loading new items for '{contentInfo.Name}'");
			try {
				var newContent = await _contentLoader.LoadContentInfoAsync(contentInfo);
				var chapters = await _chaptersRep.GetByAsync(chapter => chapter.ContentInfoId == contentInfo.Id);
				var currentChapters = contentInfo.Items;
				var currentStatus = contentInfo.Status;
				var newCollectionItems = new List<ContentItemInfo>();
				newContent.Items.ForEach(chapter => {
					if (currentChapters.Any(c => c.Name == chapter.Name)) {
						return;
					}
					newCollectionItems.Add(chapter);
					_chaptersRep.Create(chapter);
				});
				_log.LogInformation($"Found {newCollectionItems.Count} new items for '{newContent.Name}'");
				if (newCollectionItems.IsNotEmpty()) {
					_chaptersRep.Save();
					CreateNewContentNotifications(newCollectionItems);
				}
				_log.LogInformation($"Status for '{newContent.Name}'. Old: '{currentStatus}'. New: '{newContent.Status}'");
				if (newContent.Status != currentStatus) {
					SendStatusNotification(newContent);
				}
				contentInfo.Status = newContent.Status;
				_contentInfoRep.Update(contentInfo);
			} catch (Exception ex) {
				_log.LogError($"Error while loading new info for '{contentInfo.Name}'. Error info: \n{ex.Message}\n{ex.StackTrace}");
			}
		}

		public async Task LoadContentInfoAsync() {
			_log.LogInformation("LoadContentInfoAsync. Start loading content");
			var contents = await _contentInfoRep.GetAllAsync();
			await contents.ForEachAsync(async (content) => {
				await UpdateContentChaptersAsync(content);
			});
			_log.LogInformation("LoadContentInfoAsync. Ended loading content");
		}
	}
}