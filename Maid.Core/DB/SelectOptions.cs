using System.Collections.Generic;

namespace Maid.Core.DB
{
	public class OrderOptions
	{
		public string Column { get; set; }
		public bool IsAscending { get; set; } = true;
	}

	public class SelectOptions
	{
		public bool LoadLookups { get; set; }
		public int Count { get; set; } = -1;
		public int Offset { get; set; } = -1;
		public IEnumerable<OrderOptions> OrderOptions { get; set; }
	}
}