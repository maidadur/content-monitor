namespace MyHouseMaid.Models
{
	using System.Collections.Generic;

	public class Manga : BaseEntity
	{
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public decimal Rating { get; set; }
		public string Description { get; set; }
		public virtual List<Tag> Tags { get; set; }
	}
}
