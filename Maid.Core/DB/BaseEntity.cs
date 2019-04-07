namespace Maid.Core
{
	using System;

	public class BaseEntity
	{
		public Guid Id { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
