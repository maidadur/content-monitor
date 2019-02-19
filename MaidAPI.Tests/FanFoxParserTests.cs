using HtmlAgilityPack;
using MaidAPI.Html;
using MaidAPI.Manga;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaidAPI.Tests
{
	[TestClass]
	public class FanFoxParserTests
	{
		[TestMethod]
		public void GetMangaChapters() {
			var parser = new FanFoxParser();
			var loader = new HtmlDocumentLoader(null);
			string url = @"http://fanfox.net/manga/kawaii_joushi_o_komarasetai/";
			Task.Run(async () => {
				HtmlDocument htmlDoc = await loader.GetHtmlDoc(url);
				List<MangaChapterInfo> result = parser.GetMangaChapters(htmlDoc);
				Assert.AreEqual(29, result.Count);
			}).GetAwaiter().GetResult();
		}

		[TestMethod]
		public void GetMangaImageUrl() {
			var parser = new FanFoxParser();
			var loader = new HtmlDocumentLoader(null);
			string url = @"http://fanfox.net/manga/kawaii_joushi_o_komarasetai/";
			Task.Run(async () => {
				HtmlDocument htmlDoc = await loader.GetHtmlDoc(url);
				string result = parser.GetMangaImageUrl(htmlDoc);
			}).GetAwaiter().GetResult();
		}
	}
}
