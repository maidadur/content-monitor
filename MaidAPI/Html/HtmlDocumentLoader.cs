namespace MaidAPI.Html
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net.Http;
	using System.Threading.Tasks;
	using HtmlAgilityPack;
	using Microsoft.Extensions.Configuration;

	public class ServiceConfigrationSection
	{
		public Dictionary<string, string> Cookies { get; set; }
	}

	public interface IHtmlDocumentLoader
	{
		Task<HtmlDocument> GetHtmlDoc(string url);

		string ServiceName { get; set; }

	}

	public class HtmlDocumentLoader : IHtmlDocumentLoader
	{

		private IConfiguration _conf;

		public HtmlDocumentLoader(IConfiguration configuration) {
			_conf = configuration;
		}

		public string ServiceName { get; set; }

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

		private void ApplyCookies(HttpRequestMessage message) {
			if (!string.IsNullOrEmpty(ServiceName)) {
				ServiceConfigrationSection config = GetServiceConfig();
				string cookiesString = string.Empty;
				foreach (string key in config.Cookies.Keys) {
					cookiesString += string.Format("{0}={1};", key, config.Cookies[key]);
				}
				message.Headers.Add("Cookie", cookiesString);
			}
		}

		private ServiceConfigrationSection GetServiceConfig() {
			var serviceSection = _conf.GetSection(ServiceName);
			ServiceConfigrationSection config = new ServiceConfigrationSection();
			serviceSection.Bind(config);
			return config;
		}
	}
}
