namespace Maid.Manga.API.Controllers
{
	using Maid.Core;
	using Maid.Manga.Html;
	using Maid.Manga;
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;
	using Maid.Manga.API;

	[Route("api/manga")]
	[ApiController]
	public class MangaAPIController : BaseApiController
	{
		IHtmlDocumentLoader _htmlDocumentLoader;
		IParsersFactory _parsersFactory;
		ConfigHelper _configHelper;

		public MangaAPIController(IHtmlDocumentLoader htmlDocumentLoader, IParsersFactory parsersFactory, 
				ConfigHelper configHelper) {
			_htmlDocumentLoader = htmlDocumentLoader;
			_parsersFactory = parsersFactory;
			_configHelper = configHelper;
		}

		[HttpGet("Chapters/{mangaSource}")]
		public async Task<ActionResult<MangaInfo>> GetMangaChapters(string mangaSource,
				[FromQuery(Name = "url")]string url) {
			mangaSource.CheckArgumentEmptyOrNull(nameof(mangaSource));
			url.CheckArgumentEmptyOrNull(nameof(url));
			var mangaInfo = new MangaInfo();
			var config =  _configHelper.GetServiceConfig(mangaSource);
			if (config == null) {
				return mangaInfo;
			}
			_htmlDocumentLoader.Cookies = config.Cookies;
			_htmlDocumentLoader.ServiceName = mangaSource;
			var document = await _htmlDocumentLoader.GetHtmlDoc(url);
			IMangaParser mangaParser = _parsersFactory.GetParser(mangaSource);
			var chaptersList = mangaParser.GetMangaChapters(document);
			var imageUrl = mangaParser.GetMangaImageUrl(document);
			mangaInfo.ImageUrl = imageUrl;
			mangaInfo.Chapters = chaptersList;
			return mangaInfo;
		}
	}
}