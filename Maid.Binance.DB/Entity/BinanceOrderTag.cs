using Maid.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maid.Binance.DB
{
	public class BinanceOrderTag : BaseEntity
	{
		public Guid BinanceOrderId { get; set; }
		public BinanceOrder? BinanceOrder { get; set; }
		public string Tag { get; set; }
	}
}
