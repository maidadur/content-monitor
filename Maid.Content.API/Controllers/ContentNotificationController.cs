namespace Maid.Content.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Content.DB;
	using Maid.Content.ViewModels;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Identity.Web.Resource;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	[Route("api/new-content")]
	[ApiController]
	[Authorize]
	[RequiredScope(AcceptedScope = new[] { "tasks.read", "tasks.write" })]
	public class ContentNotificationController : BaseApiController<ContentItemNotification>
	{
		private IEntityRepository<ContentInfo> _contentInfoRep;

		public ContentNotificationController(
			IEntityRepository<ContentItemNotification> repository,
			IEntityRepository<ContentInfo> contentInfoRep
			)
			: base(repository) {
			_contentInfoRep = contentInfoRep;
		}

		[HttpPost("updates")]
		public async Task<ActionResult<IEnumerable<ContentNotificationViewModel>>> GetContentNotifications(SelectOptions options = null) {
			options.LoadLookups = true;
			var notifications = await EntityRepository.GetAllAsync(options);
			var contentIds = notifications.GroupBy(g => g.ContentItemInfo.ContentInfoId).Select(g => g.Key);
			var contents = await _contentInfoRep.GetByAsync(content => contentIds.Contains(content.Id));
			var models = new List<ContentNotificationViewModel>();
			notifications.ForEach(n => {
				var content = contents.First(content => content.Id == n.ContentItemInfo.ContentInfoId);
				models.Add(new ContentNotificationViewModel {
					Id = n.Id,
					CreatedOn = n.CreatedOn,
					Name = n.ContentItemInfo.Name,
					Date = n.ContentItemInfo.Date,
					Href = n.ContentItemInfo.Href,
					ContentName = content.Name,
					ImageUrl = content.ImageUrl,
					IsRead = n.IsRead
				});
			});
			return Ok(models);
		}

		[HttpPost("read")]
		public async Task<ActionResult> ReadNotifications(Guid[] notifications) {
			var items = await EntityRepository.GetByAsync(item => notifications.Contains(item.Id));
			bool save = false;
			items.ForEach(item => {
				item.IsRead = true;
				EntityRepository.Update(item);
				save = true;
			});
			if (save) {
				EntityRepository.Save();
			}
			return Ok();
		}
	}
}