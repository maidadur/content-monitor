namespace Maid.Manga.Html
{
	using HtmlAgilityPack;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IHtmlDocumentLoader
	{
		Task<HtmlDocument> GetHtmlDoc(string url);

		Dictionary<string, string> Cookies { get; set; }
	}
}