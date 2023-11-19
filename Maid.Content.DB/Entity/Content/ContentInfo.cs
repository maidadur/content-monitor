namespace Maid.Content.DB
{
	using Maid.Core;
	using Newtonsoft.Json;
	using System.Collections.Generic;

	public class ContentInfo : BaseEntity
	{
		public string Name { get; set; }

		public string ImageUrl { get; set; }

		public string Href { get; set; }

		public ContentSource Source { get; set; }

		public string Status { get; set; }

		[JsonIgnore]
		public List<ContentItemInfo> Items { get; set; }
	}
}