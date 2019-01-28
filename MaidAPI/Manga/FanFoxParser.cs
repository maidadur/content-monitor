namespace MaidAPI.Manga
{
	using HtmlAgilityPack;
	using System.Collections.Generic;
	using System.Linq;

	public class MangaChapterInfo
	{
		public string Number { get; set; }
		public string Date { get; set; }
		public string Href { get; set; }
	}

	public class MangaInfo
	{
		public string ImageUrl { get; set; }
		public List<MangaChapterInfo> Chapters { get; set; }
	}

	public interface IMangaParser {

		List<MangaChapterInfo> GetMangaChapters(HtmlDocument htmlDoc);

		string GetMangaImageUrl(HtmlDocument htmlDoc);

	}

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
					Number = chapterName,
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
