﻿// <auto-generated />
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(MyInfoContext))]
    [Migration("20190221143222_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("Domain.Entities.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MessageForGM");

                    b.Property<string>("Name");

                    b.Property<long>("Score");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Domain.Entities.Quiz", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CorrectAnswer");

                    b.Property<string>("Text");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Domain.Entities.Thing", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Fake");

                    b.Property<bool>("Loved");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Things");
                });

            modelBuilder.Entity("Domain.Entities.Title", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Fake");

                    b.Property<string>("Genres");

                    b.Property<string>("ImgUrl");

                    b.Property<string>("Name");

                    b.Property<string>("TitleType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Titles");

                    b.HasDiscriminator<string>("TitleType").HasValue("Title");
                });

            modelBuilder.Entity("Domain.Entities.Game", b =>
                {
                    b.HasBaseType("Domain.Entities.Title");

                    b.Property<int>("Metascore");

                    b.Property<string>("Plataforms");

                    b.HasDiscriminator().HasValue("Game");
                });

            modelBuilder.Entity("Domain.Entities.Movie", b =>
                {
                    b.HasBaseType("Domain.Entities.Title");

                    b.Property<int>("Length");

                    b.Property<int>("Score");

                    b.HasDiscriminator().HasValue("Movie");
                });

            modelBuilder.Entity("Domain.Entities.MusicBand", b =>
                {
                    b.HasBaseType("Domain.Entities.Title");

                    b.Property<string>("FavoriteSongs");

                    b.Property<string>("Members");

                    b.HasDiscriminator().HasValue("MusicBand");
                });
#pragma warning restore 612, 618
        }
    }
}