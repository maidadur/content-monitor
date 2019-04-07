namespace Maid.Manga
{
	using HtmlAgilityPack;
	using System.Collections.Generic;

	public interface IMangaParser
	{

		List<MangaChapterInfo> GetMangaChapters(HtmlDocument htmlDoc);

		string GetMangaImageUrl(HtmlDocument htmlDoc);

	}
}
