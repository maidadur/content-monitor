using Maid.Core;
using Maid.Manga.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maid.Manga.ViewModels
{
	public class MangaChapterNotificationViewModel : BaseEntity
	{
		public string Name { get; set; }

		public string Date { get; set; }

		public string Href { get; set; }

		public string MangaName { get; set; }

		public string ImageUrl { get; set; }
	}
}
