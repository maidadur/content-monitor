using System;

namespace Maid.Core
{
	[AttributeUsage(System.AttributeTargets.Class)]
	public class MessageConsumerAttribute : Attribute
	{
		public string Queue { get; set; }
	}
}