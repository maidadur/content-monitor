namespace Maid.Manga.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using Maid.Manga.ViewModels;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	[Route("api/new-manga")]
	[ApiController]
	[Authorize]
	public class MangaChapterNotificationController : BaseApiController<MangaChapterNotification>
	{
		private IEntityRepository<MangaInfo> _mangaRep;

		public MangaChapterNotificationController(
			IEntityRepository<MangaChapterNotification> repository,
			IEntityRepository<MangaInfo> mangaRep
			)
			: base(repository) {
			_mangaRep = mangaRep;
		}

		[HttpPost("updates")]
		public async Task<ActionResult<IEnumerable<MangaChapterNotificationViewModel>>> GetMangaNotifications(SelectOptions options = null) {
			options.LoadLookups = true;
			var notifications = await EntityRepository.GetAllAsync(options);
			var mangaIds = notifications.GroupBy(g => g.MangaChapterInfo.MangaId).Select(g => g.Key);
			var mangas = await _mangaRep.GetByAsync(manga => mangaIds.Contains(manga.Id));
			var models = new List<MangaChapterNotificationViewModel>();
			notifications.ForEach(n => {
				var manga = mangas.First(manga => manga.Id == n.MangaChapterInfo.MangaId);
				models.Add(new MangaChapterNotificationViewModel {
					Id = n.Id,
					CreatedOn = n.CreatedOn,
					Name = n.MangaChapterInfo.Name,
					Date = n.MangaChapterInfo.Date,
					Href = n.MangaChapterInfo.Href,
					MangaName = manga.Name,
					ImageUrl = manga.ImageUrl,
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