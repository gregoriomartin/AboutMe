using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Domain
{
    public class MyInfoContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MusicBand> MusicBands { get; set; }
        public DbSet<Thing> Things { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Quiz> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=aboutMe.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });


            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>()
                .HasDiscriminator<string>("TitleType");

            modelBuilder.Entity<Title>()
            .Property(e => e.Genres)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<Game>()
            .Property(e => e.Plataforms)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            

            modelBuilder.Entity<MusicBand>()
            .Property(e => e.Genres)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<MusicBand>()
            .Property(e => e.FavoriteSongs)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<MusicBand>()
            .Property(e => e.Members)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
