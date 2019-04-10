namespace Maid.Manga.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga;
	using Maid.Manga.API;
	using Maid.Manga.DB;
	using Maid.Manga.Html;
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	[Route("api/manga")]
	[ApiController]
	public class MangaInfoController : BaseApiController<MangaInfo>
	{
		IHtmlDocumentLoader _htmlDocumentLoader;
		IParsersFactory _parsersFactory;
		ConfigHelper _configHelper;

		public MangaInfoController(IEntityRepository<MangaInfo> entityRepository) 
			: base(entityRepository) {
		}

		//[HttpPost("LoadChapters")]
		//public async Task<ActionResult<MangaInfo>> LoadChapters(MangaInfo item) {
		//	mangaSource.CheckArgumentEmptyOrNull(nameof(mangaSource));
		//	url.CheckArgumentEmptyOrNull(nameof(url));
		//	var mangaInfo = new MangaInfo();
		//	var config = _configHelper.GetServiceConfig(mangaSource);
		//	if (config == null) {
		//		return mangaInfo;
		//	}
		//	_htmlDocumentLoader.Cookies = config.Cookies;
		//	_htmlDocumentLoader.ServiceName = mangaSource;
		//	var document = await _htmlDocumentLoader.GetHtmlDoc(url);
		//	IMangaParser mangaParser = _parsersFactory.GetParser(mangaSource);
		//	var chaptersList = mangaParser.GetMangaChapters(document);
		//	var imageUrl = mangaParser.GetMangaImageUrl(document);
		//	mangaInfo.ImageUrl = imageUrl;
		//	mangaInfo.Chapters = chaptersList;
		//	return mangaInfo;
		//}
	}
}