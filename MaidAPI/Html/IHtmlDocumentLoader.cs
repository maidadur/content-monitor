namespace MaidAPI.Html
{
	using System.Threading.Tasks;
	using HtmlAgilityPack;

	public interface IHtmlDocumentLoader
	{
		Task<HtmlDocument> GetHtmlDoc(string url);

		string ServiceName { get; set; }

	}
}
