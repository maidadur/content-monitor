namespace Maid.Manga
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using Maid.Manga.Html;
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading.Tasks;

	public class MangaLoader
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

		public async Task<MangaInfo> LoadMangaInfoAsync(MangaInfo mangaInfo) {
			mangaInfo.CheckArgumentNull(nameof(mangaInfo));
			string mangaUrl = mangaInfo.Href;
			Uri mangaUri = new Uri(mangaUrl);
			MangaSource mangaSource = await GetSourceByUrl(mangaUri);
			mangaInfo.Source = mangaSource ?? throw new ArgumentException("Wrong url domain");
			string sourceName = mangaSource.Name;
			var config = _configHelper.GetServiceConfig(mangaSource.Name);
			if (config == null) {
				return mangaInfo;
			}
			_htmlDocumentLoader.Cookies = config.Cookies;
			_htmlDocumentLoader.ServiceName = sourceName;
			var document = await _htmlDocumentLoader.GetHtmlDoc(mangaInfo.Href);
			IMangaParser mangaParser = _parsersFactory.GetParser(sourceName);
			List<MangaChapterInfo> chaptersList = mangaParser.GetMangaChapters(document);
			string imageUrl = mangaParser.GetMangaImageUrl(document);
			string name = mangaParser.GetMangaName(document);
			mangaInfo.ImageUrl = imageUrl;
			mangaInfo.Chapters = chaptersList;
			mangaInfo.Name = name;
			return mangaInfo;
		}
	}
}
