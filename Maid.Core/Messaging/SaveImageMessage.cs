﻿using System;

namespace Maid.Core
{
	public class SaveImageMessage
	{
		public string ImageUrl { get; set; }
		public string EntityName { get; set; }
		public Guid EntityId { get; set; }
	}
}
