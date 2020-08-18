namespace Maid.Manga.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using Maid.Manga.ViewModels;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	[EnableCors("AllowOrigin")]
	[Route("api/new-manga")]
	[ApiController]
	public class MangaChapterNotificationController : BaseApiController<MangaChapterNotification>
	{
		private IEntityRepository<MangaInfo> _mangaRep;

		public MangaChapterNotificationController(
			IEntityRepository<MangaChapterNotification> repository,
			IEntityRepository<MangaInfo> mangaRep
			)
			: base (repository) {
			_mangaRep = mangaRep;
		}

		[HttpGet("updates")]
		public async Task<ActionResult<IEnumerable<MangaChapterNotificationViewModel>>> GetMangaNotifications(int count = 0, int offset = 0) {
			var notifications = await EntityRepository.GetAllAsync(new SelectOptions {
				LoadLookups = true
			});
			var mangaIds = notifications.GroupBy(g => g.MangaChapterInfo.MangaId).Select(g => g.Key);
			var mangas = await _mangaRep.GetByAsync(manga => mangaIds.Contains(manga.Id));
			var models = new List<MangaChapterNotificationViewModel>();
			notifications.ForEach(n => {
				var manga = mangas.First(manga => manga.Id == n.MangaChapterInfo.MangaId);
				models.Add(new MangaChapterNotificationViewModel {
					CreatedOn = n.CreatedOn,
					Name = n.MangaChapterInfo.Name,
					Date = n.MangaChapterInfo.Date,
					Href = n.MangaChapterInfo.Href,
					MangaName = manga.Name,
					ImageUrl = manga.ImageUrl
				});
			});
			return Ok(models);
		}
	}
}
