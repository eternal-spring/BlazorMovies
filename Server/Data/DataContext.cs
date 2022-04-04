using Microsoft.EntityFrameworkCore;
using BlazorMovies.Shared;
using System;
using System.Collections.Generic;

namespace BlazorMovies.Server.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Actor> Actors { get; set; } = null!;
        public DbSet<Director> Directors { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<Movie>()
                    .Ignore(m => m.Actors)
                    .Ignore(m => m.Directors)
                    .Ignore(m => m.Tags)
                    ;
                modelBuilder.Entity<Actor>()
                    .HasMany(a => a.Movies)
                    .WithMany(m => m.Actors)
                    .UsingEntity<Dictionary<string, object>>(
                        "ActorMovie",
                        j => j
                            .HasOne<Movie>()
                            .WithMany()
                            .HasForeignKey("ImdbId")
                            .HasConstraintName("FK_ActorMovie_Movies_ImdbId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j
                            .HasOne<Actor>()
                            .WithMany()
                            .HasForeignKey("PersonId")
                            .HasConstraintName("FK_ActorMovie_Actors_PersonId")
                            .OnDelete(DeleteBehavior.Cascade));
                modelBuilder.Entity<Director>()
                    .HasMany(d => d.Movies)
                    .WithMany(m => m.Directors)
                    .UsingEntity<Dictionary<string, object>>(
                        "DirectorMovie",
                        j => j
                            .HasOne<Movie>()
                            .WithMany()
                            .HasForeignKey("ImdbId")
                            .HasConstraintName("FK_DirectorMovie_Movies_ImdbId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j
                            .HasOne<Director>()
                            .WithMany()
                            .HasForeignKey("PersonId")
                            .HasConstraintName("FK_DirectorMovie_Directors_PersonId")
                            .OnDelete(DeleteBehavior.Cascade));
                modelBuilder.Entity<Tag>()
                    .HasMany(t => t.Movies)
                    .WithMany(m => m.Tags)
                    .UsingEntity<Dictionary<string, object>>(
                        "TagMovie",
                        j => j
                            .HasOne<Movie>()
                            .WithMany()
                            .HasForeignKey("ImdbId")
                            .HasConstraintName("FK_TagMovie_Movies_ImdbId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j
                            .HasOne<Tag>()
                            .WithMany()
                            .HasForeignKey("TagId")
                            .HasConstraintName("FK_TagMovie_Tags_TagId")
                            .OnDelete(DeleteBehavior.Cascade));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
