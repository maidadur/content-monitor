namespace Maid.Content
{
	using HtmlAgilityPack;
	using Maid.Content.DB;
	using System.Collections.Generic;
	using System.Linq;

	public class DefaultPageParser : IContentParser
	{
		public List<ContentItemInfo> GetCollectionItems(HtmlDocument htmlDoc, ContentSource source) {
			var chaptersList = new List<ContentItemInfo>();
			try {
				HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes(source.CollectionItemXpath);
				foreach (var item in nodes) {
					var chapterXpath = item.XPath;
					var titleElems = htmlDoc.DocumentNode.SelectNodes(chapterXpath + source.CollectionItemTitleXpath);
					var hrefElements = htmlDoc.DocumentNode.SelectNodes(chapterXpath + source.CollectionItemHrefXpath);
					var dataElems = htmlDoc.DocumentNode.SelectNodes(chapterXpath + source.CollectionItemDateXpath);
					string chapterName = titleElems.FirstOrDefault()?.InnerText;
					string releaseDate = dataElems.FirstOrDefault()?.InnerText;
					string href = hrefElements.FirstOrDefault()?.GetAttributeValue("href", "");
					chaptersList.Add(new ContentItemInfo {
						Name = chapterName,
						Date = releaseDate,
						Href = href
					});
				}
			} catch { }
			return chaptersList;
		}

		public string GetStatus(HtmlDocument htmlDoc, ContentSource source) {
			string name = "";
			try {
				HtmlNode statusNode = htmlDoc.DocumentNode.SelectSingleNode(source.StatusXpath);
				name = statusNode.InnerText;
			} catch { }
			return name;
		}

		public string GetImageUrl(HtmlDocument htmlDoc, string xpath) {
			string imageUrl = "";
			try {

				if (xpath == null) {
					return "";
				}
				HtmlNode imgNode = htmlDoc.DocumentNode.SelectSingleNode(xpath);
				imageUrl = imgNode.GetAttributeValue("src", string.Empty);
			} catch { }
			return imageUrl;
		}

		public string GetContentTitle(HtmlDocument htmlDoc, string xpath) {
			string name = "";
			try {

				if (xpath == null) {
					return "";
				}
				HtmlNode nameNode = htmlDoc.DocumentNode.SelectSingleNode(xpath);
				name = nameNode.InnerText;
			} catch { }
			return name;
		}
	}
}