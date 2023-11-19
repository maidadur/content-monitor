namespace Maid.Content.DB
{
	using Maid.Core;
	using System;
	using System.ComponentModel.DataAnnotations;

	public class ContentItemInfo : BaseEntity
	{
		public string Name { get; set; }

		public string Date { get; set; }

		public string Href { get; set; }

		[Required]
		public ContentInfo ContentInfo { get; set; }

		public Guid ContentInfoId { get; set; }
	}
}