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

		private async Task<MangaSource> GetSourceByUrl(Uri mangaUri) {
			var sourceItem = await _sourceRepository.GetByAsync(i => new Uri(i.DomainUrl).Host == mangaUri.Host);
			MangaSource mangaSource = sourceItem.FirstOrDefault();
			return mangaSource;
		}

		protected virtual void FillMangaInfo(MangaInfo mangaInfo, string sourceName, HtmlDocument document) {
			IMangaParser mangaParser = _parsersFactory.GetParser(sourceName);
			List<MangaChapterInfo> chaptersList = mangaParser.GetMangaChapters(document);
			string imageUrl = mangaParser.GetMangaImageUrl(document);
			string name = mangaParser.GetMangaName(document);
			mangaInfo.ImageUrl = imageUrl;
			mangaInfo.Chapters = chaptersList;
			mangaInfo.Name = name;
		}

		public async Task<MangaInfo> LoadMangaInfoAsync(MangaInfo mangaInfo) {
			mangaInfo.CheckArgumentNull(nameof(mangaInfo));
			string mangaUrl = mangaInfo.Href;
			mangaUrl.CheckArgumentEmptyOrNull(nameof(mangaUrl));
			Uri mangaUri = new Uri(mangaUrl);
			MangaSource mangaSource = await GetSourceByUrl(mangaUri);
			mangaInfo.Source = mangaSource ?? throw new ArgumentException("Wrong url domain");
			string sourceName = mangaSource.Name;
			ServiceConfigrationSection config = _configHelper.GetServiceConfig(sourceName);
			if (config == null) {
				throw new ArgumentException($"No handler for source {sourceName}");
			}
			_htmlDocumentLoader.Cookies = config.Cookies;
			_htmlDocumentLoader.ServiceName = sourceName;
			HtmlDocument document = await _htmlDocumentLoader.GetHtmlDoc(mangaInfo.Href);
			FillMangaInfo(mangaInfo, sourceName, document);
			return mangaInfo;
		}
	}
}
