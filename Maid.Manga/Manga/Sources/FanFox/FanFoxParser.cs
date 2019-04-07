namespace Maid.Manga
{
	using HtmlAgilityPack;
	using System.Collections.Generic;
	using System.Linq;

	public class FanFoxParser : IMangaParser
	{
		public List<MangaChapterInfo> GetMangaChapters(HtmlDocument htmlDoc) {
			HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='detail-main-list-main']");
			var chaptersList = new List<MangaChapterInfo>();
			foreach (var item in nodes) {
				var elems = item.ChildNodes.Where(cn => cn.Name == "p").ToList();
				string chapterName = elems.FirstOrDefault()?.InnerText;
				string releaseDate = elems.LastOrDefault()?.InnerText;
				string href = item.ParentNode.GetAttributeValue("href", "");
				chaptersList.Add(new MangaChapterInfo {
					Name = chapterName,
					Date = releaseDate,
					Href = "http://fanfox.net" + href
				});
			}
			return chaptersList;
		}

		public string GetMangaImageUrl(HtmlDocument htmlDoc) {
			HtmlNode imgNode = htmlDoc.DocumentNode.SelectSingleNode("//img[@class='detail-info-cover-img']");
			string imageUrl = imgNode.GetAttributeValue("src", string.Empty);
			return imageUrl;
		}
	}
}
