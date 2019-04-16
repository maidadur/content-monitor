namespace Maid.Manga.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga;
	using Maid.Manga.API;
	using Maid.Manga.DB;
	using Maid.Manga.Html;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Threading.Tasks;

	[Route("api/manga")]
	[ApiController]
	public class MangaInfoController : BaseApiController<MangaInfo>
	{
		private MangaLoader _mangaLoader;

		public MangaInfoController(IEntityRepository<MangaInfo> entityRepository, MangaLoader mangaLoader)
			: base(entityRepository) {
			_mangaLoader = mangaLoader;
		}

		[HttpPost("LoadMangaInfo")]
		public async Task<ActionResult> LoadMangaInfo([FromBody]MangaInfo item) {
			item = await EntityRepository.GetAsync(item.Id);
			if (item == null) {
				return BadRequest("nema");
			}
			item = await _mangaLoader.LoadMangaInfoAsync(item);
			EntityRepository.Update(item);
			EntityRepository.Save();
			return Ok();
		}
	}
}