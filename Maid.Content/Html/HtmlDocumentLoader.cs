﻿namespace Maid.Content.Html
{
	using HtmlAgilityPack;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net.Http;
	using System.Threading.Tasks;

	public class HtmlDocumentLoader : IHtmlDocumentLoader
	{
		public Dictionary<string, string> Cookies { get; set; }

		private void ApplyCookies(HttpRequestMessage message) {
			if (Cookies != null) {
				string cookiesString = string.Empty;
				foreach (string key in Cookies?.Keys) {
					cookiesString += string.Format("{0}={1};", key, Cookies[key]);
				}
				message.Headers.Add("Cookie", cookiesString);
			}
		}

		public async Task<HtmlDocument> GetHtmlDoc(string url) {
			HtmlDocument doc = new HtmlDocument();
			var baseAddress = new Uri(url);
			using (var handler = new HttpClientHandler { UseCookies = true })
			using (var client = new HttpClient(handler) { BaseAddress = baseAddress }) {
				var message = new HttpRequestMessage(HttpMethod.Get, url);
				ApplyCookies(message);
				var result = await client.SendAsync(message);
				result.EnsureSuccessStatusCode();
				Stream stream = await result.Content.ReadAsStreamAsync();
				doc.Load(stream);
			}
			return doc;
		}
	}
}