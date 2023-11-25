namespace Maid.Content
{
	using HtmlAgilityPack;
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Content.DB;
	using Maid.Content.Html;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class ContentLoader : IContentLoader
	{
		private IHtmlDocumentLoader _htmlDocumentLoader;
		private IParsersFactory _parsersFactory;
		private ConfigHelper _configHelper;
		private IEntityRepository<ContentSource> _sourceRepository;

		public ContentLoader(IHtmlDocumentLoader documentLoader, IParsersFactory parsersFactory,
				IEntityRepository<ContentSource> sourceRepository, ConfigHelper configHelper) {
			_htmlDocumentLoader = documentLoader;
			_parsersFactory = parsersFactory;
			_configHelper = configHelper;
			_sourceRepository = sourceRepository;
		}

		private ContentSource GetSourceByUrl(Uri contentUri) {
			var sources = _sourceRepository.GetAll();
			var sourceItem = sources.FirstOrDefault(i => i.DomainUrl == contentUri.Host);
			return sourceItem;
		}

		protected virtual ContentInfo FillContentInfo(ContentInfo contentInfo, ContentSource source, HtmlDocument document) {
			IContentParser contentParser = _parsersFactory.GetParser(source.Code);
			string imageUrl = contentParser.GetImageUrl(document, source.ImageXpath);
			string name = contentParser.GetContentTitle(document, source.TitleXpath);
			var content = new ContentInfo();
			content.Id = contentInfo.Id;
			content.ImageUrl = imageUrl;
			content.Name = name;
			content.Href = contentInfo.Href;
			content.Source = source;
			List<ContentItemInfo> collectionItems = contentParser.GetCollectionItems(document, source);
			collectionItems.Reverse();
			collectionItems.ForEach(chapter => chapter.ContentInfoId = contentInfo.Id);
			content.Items = collectionItems;
			content.Status = contentParser.GetStatus(document, source) ?? "";
			content.IsStatusPositive = source.PositiveStatusText.IsNullOrEmpty() ? content.Status.Contains(source.PositiveStatusText) : false;
			return content;
		}

		public async Task<ContentInfo> LoadContentInfoAsync(ContentInfo contentInfo) {
			contentInfo.CheckArgumentNull(nameof(contentInfo));
			string contentUrl = contentInfo.Href;
			contentUrl.CheckArgumentEmptyOrNull(nameof(contentUrl));
			Uri contentUri = new Uri(contentUrl);
			ContentSource contentSource = GetSourceByUrl(contentUri);
			if (contentSource == null) {
				throw new ArgumentException("Wrong url domain");
			}
			string sourceName = contentSource.Name;
			ServiceConfigrationSection config = _configHelper.GetServiceConfig(sourceName);
			if (config == null) {
				throw new ArgumentException($"No handler for source {sourceName}");
			}
			_htmlDocumentLoader.Cookies = config.Cookies;
			HtmlDocument document = await _htmlDocumentLoader.GetHtmlDoc(contentUrl);
			return FillContentInfo(contentInfo, contentSource, document);
		}
	}
}