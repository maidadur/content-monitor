namespace Maid.Manga
{

	using HtmlAgilityPack;
	using Maid.Manga.DB;
	using System.Collections.Generic;
	using System.Linq;

	public class DefaultPageParser : IMangaParser
	{
		public List<MangaChapterInfo> GetMangaChapters(HtmlDocument htmlDoc, MangaSource source) {
			var chaptersList = new List<MangaChapterInfo>();
			try {
				HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes(source.ChapterXpath);
				foreach (var item in nodes) {
					var chapterXpath = item.XPath;
					var titleElems = htmlDoc.DocumentNode.SelectNodes(chapterXpath + source.ChapterTitleXpath);
					var hrefElements = htmlDoc.DocumentNode.SelectNodes(chapterXpath + source.ChapterHrefXpath);
					var dataElems = htmlDoc.DocumentNode.SelectNodes(chapterXpath + source.ChapterDateXpath);
					string chapterName = titleElems.FirstOrDefault()?.InnerText;
					string releaseDate = dataElems.FirstOrDefault()?.InnerText;
					string href = hrefElements.FirstOrDefault()?.GetAttributeValue("href", "");
					chaptersList.Add(new MangaChapterInfo {
						Name = chapterName,
						Date = releaseDate,
						Href = href
					});
				}
			} catch { }
			return chaptersList;
		}

		public string GetMangaImageUrl(HtmlDocument htmlDoc, string xpath) {
			if (xpath == null) {
				return "";
			}
			HtmlNode imgNode = htmlDoc.DocumentNode.SelectSingleNode(xpath);
			string imageUrl = imgNode.GetAttributeValue("src", string.Empty);
			return imageUrl;
		}

		public string GetMangaName(HtmlDocument htmlDoc, string xpath) {
			if (xpath == null) {
				return "";
			}
			HtmlNode nameNode = htmlDoc.DocumentNode.SelectSingleNode(xpath);
			string name = nameNode.InnerText;
			return name;
		}
	}
}
