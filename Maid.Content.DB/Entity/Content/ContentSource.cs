using Maid.Core;

namespace Maid.Content.DB
{
	public class ContentSource : BaseLookup
	{
		public string DomainUrl { get; set; }

		public string ImageUrl { get; set; }

		public string TitleXpath { get; set; }

		public string ImageXpath { get; set; }

		public string StatusXpath { get; set; }

		public string CollectionItemXpath { get; set; }

		public string CollectionItemHrefXpath { get; set; }

		public string CollectionItemTitleXpath { get; set; }

		public string CollectionItemDateXpath { get; set; }
	}
}
