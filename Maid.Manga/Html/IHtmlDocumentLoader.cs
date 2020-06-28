namespace Maid.Manga.Html
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using HtmlAgilityPack;

	public interface IHtmlDocumentLoader
	{
		Task<HtmlDocument> GetHtmlDoc(string url);

		Dictionary<string, string> Cookies { get; set; }

	}
}
