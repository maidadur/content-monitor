﻿// <auto-generated />
using System;
using Maid.Manga.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Maid.Manga.DB.Migrations
{
    [DbContext(typeof(MangaDbContext))]
    [Migration("20200815104350_MangaNotificationsV1")]
    partial class MangaNotificationsV1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Maid.Manga.DB.MangaChapterInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Href")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MangaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MangaId");

                    b.ToTable("MangaChapterInfo");
                });

            modelBuilder.Entity("Maid.Manga.DB.MangaChapterNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<Guid>("MangaChapterInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MangaChapterInfoId");

                    b.ToTable("MangaChapterNotification");
                });

            modelBuilder.Entity("Maid.Manga.DB.MangaInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Href")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SourceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("MangaInfo");
                });

            modelBuilder.Entity("Maid.Manga.DB.MangaSource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChapterDateXpath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChapterHrefXpath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChapterTitleXpath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChapterXpath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("DomainUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageXpath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleXpath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MangaSource");
                });

            modelBuilder.Entity("Maid.Manga.DB.MangaChapterInfo", b =>
                {
                    b.HasOne("Maid.Manga.DB.MangaInfo", "Manga")
                        .WithMany("Chapters")
                        .HasForeignKey("MangaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Maid.Manga.DB.MangaChapterNotification", b =>
                {
                    b.HasOne("Maid.Manga.DB.MangaChapterInfo", "MangaChapterInfo")
                        .WithMany()
                        .HasForeignKey("MangaChapterInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Maid.Manga.DB.MangaInfo", b =>
                {
                    b.HasOne("Maid.Manga.DB.MangaSource", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId");
                });
#pragma warning restore 612, 618
        }
    }
}