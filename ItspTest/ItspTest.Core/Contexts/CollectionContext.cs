using ItspTest.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ItspTest.Core.Contexts
{
    public class CollectionContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserCollection> UserCollections { get; set; }
        public DbSet<UserMovieCollection> UserMovieCollections { get; set; }

        public CollectionContext(DbContextOptions<CollectionContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMovieCollection>().HasKey(sc => new { sc.MovieId, sc.UserCollectionId });

            modelBuilder.Entity<UserMovieCollection>()
                .HasOne(sc => sc.Movie)
                .WithMany(s => s.UserMovieCollections)
                .HasForeignKey(sc => sc.MovieId);

            modelBuilder.Entity<UserMovieCollection>()
                .HasOne(sc => sc.UserCollection)
                .WithMany(s => s.UserMovieCollections)
                .HasForeignKey(sc => sc.UserCollectionId);
        }
    }
}