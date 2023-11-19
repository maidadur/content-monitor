namespace Maid.Content
{
	using HtmlAgilityPack;
	using Maid.Content.DB;
	using System.Collections.Generic;

	public interface IContentParser
	{
		List<ContentItemInfo> GetCollectionItems(HtmlDocument htmlDoc, ContentSource source);

		string GetStatus(HtmlDocument htmlDoc, ContentSource source);

		string GetImageUrl(HtmlDocument htmlDoc, string xpath);

		string GetContentTitle(HtmlDocument htmlDoc, string xpath);
	}
}