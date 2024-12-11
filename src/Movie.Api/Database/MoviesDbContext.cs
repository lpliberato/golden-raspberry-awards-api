using Api.Helpers;
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
            SeedMoviesFromCsv(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedMoviesFromCsv(ModelBuilder modelBuilder)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "Database", "movielist.csv");
            if (File.Exists(filePath))
            {
                var movies = CsvReaderHelper.ReadCsv(filePath);
                var movieId = 1;
                movies.ForEach(movie => movie.Id = movieId++);
                modelBuilder.Entity<Movie>().HasData(movies);
            }
        }
    }
}
