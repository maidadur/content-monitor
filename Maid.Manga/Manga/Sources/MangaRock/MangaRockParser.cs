namespace Maid.Manga
{
	using HtmlAgilityPack;
	using Maid.Manga.DB;
	using System.Collections.Generic;
	using System.Linq;

	public class MangaRockParser : IMangaParser
	{
		public List<MangaChapterInfo> GetMangaChapters(HtmlDocument htmlDoc) {
			HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[@id='chapters-list']//table/tbody/tr");
			var chaptersList = new List<MangaChapterInfo>();
			foreach (var item in nodes) {
				var elems = item.ChildNodes.Where(cn => cn.Name == "td").ToList();
				string chapterName = elems.FirstOrDefault()?.InnerText;
				string releaseDate = elems.ElementAt(1)?.InnerText;
				string href = elems.FirstOrDefault().FirstChild.GetAttributeValue("href", "");
				chaptersList.Add(new MangaChapterInfo {
					Name = chapterName,
					Date = releaseDate,
					Href = "https://mangarock.com" + href
				});
			}
			return chaptersList;
		}

		public string GetMangaImageUrl(HtmlDocument htmlDoc) {
			HtmlNode imgNode = htmlDoc.DocumentNode.SelectSingleNode("//img[@itemprop='url']");
			string imageUrl = imgNode.GetAttributeValue("src", string.Empty);
			return imageUrl;
		}

		public string GetMangaName(HtmlDocument htmlDoc) {
			HtmlNode nameNode = htmlDoc.DocumentNode.SelectSingleNode("//h1[@itemprop='name headline']");
			string name = nameNode.InnerText;
			return name;
		}
	}
}
