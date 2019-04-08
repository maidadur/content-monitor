namespace Maid.Core
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class BaseEntity
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
