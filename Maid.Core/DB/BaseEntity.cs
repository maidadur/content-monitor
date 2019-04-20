namespace Maid.Core
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime? CreatedOn { get; set; } = DateTime.Now;
	}
}
