namespace Maid.Manga
{
	using HtmlAgilityPack;
	using Maid.Manga.DB;
	using System.Collections.Generic;

	public interface IMangaParser
	{

		List<MangaChapterInfo> GetMangaChapters(HtmlDocument htmlDoc, MangaSource source);

		string GetMangaImageUrl(HtmlDocument htmlDoc, string xpath);

		string GetMangaName(HtmlDocument htmlDoc, string xpath);

	}
}
