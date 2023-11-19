namespace Maid.Content.DB
{
	using Maid.Core;
	using System;

	public class ContentItemNotification : BaseNotification
	{
		public ContentItemInfo ContentItemInfo { get; set; }
		public Guid ContentItemInfoId { get; set; }
	}
}