using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Database
{
    public class MoviesDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasKey(m => m.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
