using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyHouseMaid.Models;

namespace MyHouseMaid.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) {
		}

		public DbSet<Manga> Manga { get; set; }
		public DbSet<MangaChapter> MangaChapters { get; set; }
		public DbSet<Tag> Tags { get; set; }
	}
}
