namespace Maid.Manga.Html
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using HtmlAgilityPack;

	public interface IHtmlDocumentLoader
	{
		Task<HtmlDocument> GetHtmlDoc(string url);

		string ServiceName { get; set; }

		Dictionary<string, string> Cookies { get; set; }

	}
}
