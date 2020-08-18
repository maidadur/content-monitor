namespace Maid.Manga
{
	using HtmlAgilityPack;
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using Maid.Manga.Html;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class MangaLoader : IMangaLoader
	{
		IHtmlDocumentLoader _htmlDocumentLoader;
		IParsersFactory _parsersFactory;
		ConfigHelper _configHelper;
		IEntityRepository<MangaSource> _sourceRepository;

		public MangaLoader(IHtmlDocumentLoader documentLoader, IParsersFactory parsersFactory,
				IEntityRepository<MangaSource> sourceRepository, ConfigHelper configHelper) {
			_htmlDocumentLoader = documentLoader;
			_parsersFactory = parsersFactory;
			_configHelper = configHelper;
			_sourceRepository = sourceRepository;
		}

		private MangaSource GetSourceByUrl(Uri mangaUri) {
			var sources = _sourceRepository.GetAll();
			var sourceItem = sources.FirstOrDefault(i => i.DomainUrl == mangaUri.Host);
			return sourceItem;
		}

		protected virtual MangaInfo FillMangaInfo(MangaInfo mangaInfo, MangaSource source, HtmlDocument document) {
			IMangaParser mangaParser = _parsersFactory.GetParser(source.Code);
			List<MangaChapterInfo> chaptersList = mangaParser.GetMangaChapters(document, source);
			chaptersList.ForEach(chapter => chapter.MangaId = mangaInfo.Id);
			string imageUrl = mangaParser.GetMangaImageUrl(document, source.ImageXpath);
			string name = mangaParser.GetMangaName(document, source.TitleXpath);
			var manga = new MangaInfo();
			manga.Id = mangaInfo.Id;
			manga.ImageUrl = imageUrl;
			manga.Chapters = chaptersList;
			manga.Name = name;
			manga.Href = mangaInfo.Href;
			manga.Source = mangaInfo.Source;
			return manga;
		}

		public async Task<MangaInfo> LoadMangaInfoAsync(MangaInfo mangaInfo) {
			mangaInfo.CheckArgumentNull(nameof(mangaInfo));
			string mangaUrl = mangaInfo.Href;
			mangaUrl.CheckArgumentEmptyOrNull(nameof(mangaUrl));
			Uri mangaUri = new Uri(mangaUrl);
			MangaSource mangaSource = GetSourceByUrl(mangaUri);
			mangaInfo.Source = mangaSource ?? throw new ArgumentException("Wrong url domain");
			string sourceName = mangaSource.Name;
			ServiceConfigrationSection config = _configHelper.GetServiceConfig(sourceName);
			if (config == null) {
				throw new ArgumentException($"No handler for source {sourceName}");
			}
			_htmlDocumentLoader.Cookies = config.Cookies;
			HtmlDocument document = await _htmlDocumentLoader.GetHtmlDoc(mangaInfo.Href);
			return FillMangaInfo(mangaInfo, mangaSource, document);
		}
	}
}
