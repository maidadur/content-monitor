﻿namespace MaidAPI.Controllers
{
	using MaidAPI.Html;
	using MaidAPI.Manga;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Threading.Tasks;

	[Route("api/manga")]
	[ApiController]
	public class MangaAPIController : ControllerBase
	{
		IHtmlDocumentLoader _htmlDocumentLoader;
		IParsersFactory _parsersFactory;

		public MangaAPIController(IHtmlDocumentLoader htmlDocumentLoader, IParsersFactory parsersFactory) {
			_htmlDocumentLoader = htmlDocumentLoader;
			_parsersFactory = parsersFactory;
		}

		[HttpGet("GetChaptersList/{mangaSource}")]
		public async Task<ActionResult<MangaInfo>> GetChaptersList(string mangaSource,
				[FromQuery(Name = "url")]string url) {
			if (string.IsNullOrEmpty(mangaSource)) {
				throw new ArgumentException("mangaSource");
			}
			_htmlDocumentLoader.ServiceName = mangaSource;
			var document = await _htmlDocumentLoader.GetHtmlDoc(url);
			IMangaParser mangaParser = _parsersFactory.GetParser(mangaSource);
			var chaptersList = mangaParser.GetMangaChapters(document);
			var imageUrl = mangaParser.GetMangaImageUrl(document);
			return new MangaInfo() {
				ImageUrl = imageUrl,
				Chapters = chaptersList
			};
		}

		[HttpGet("")]
		public ActionResult Ping() {
			return Ok();
		}
	}
}