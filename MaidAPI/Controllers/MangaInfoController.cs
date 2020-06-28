namespace Maid.Manga.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga;
	using Maid.Manga.DB;
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	[Route("api/manga")]
	[ApiController]
	public class MangaInfoController : BaseApiController<MangaInfo>
	{
		private IMangaLoader _mangaLoader;
		private IEntityRepository<MangaChapterInfo> _chaptersRep;

		public MangaInfoController(
				IEntityRepository<MangaInfo> mangaInfoRep,
				IEntityRepository<MangaChapterInfo> chaptersRep,
				IMangaLoader mangaLoader) : base(mangaInfoRep) {
			_mangaLoader = mangaLoader;
			_chaptersRep = chaptersRep;
		}

		[HttpPost("LoadMangaInfo")]
		public async Task<ActionResult> LoadMangaInfo([FromBody]MangaInfo item) {
			item = await EntityRepository.GetAsync(item.Id);
			if (item == null) {
				return BadRequest("net takogo");
			}
			item = await _mangaLoader.LoadMangaInfoAsync(item);
			_chaptersRep.Delete(chapter => chapter.Manga.Id == item.Id);
			EntityRepository.Update(item);
			EntityRepository.Save();
			return Ok();
		}
	}
}