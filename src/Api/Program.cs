using Microsoft.EntityFrameworkCore;
using Api.Database;
using Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MoviesDbContext>(options =>
    options.UseSqlite("DataSource=moviesDb"));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var _context = scope.ServiceProvider.GetRequiredService<MoviesDbContext>();

    string filePath = Path.Combine(AppContext.BaseDirectory, "Database", "movielist.csv");
    if (File.Exists(filePath))
    {
        var movies = CsvReaderHelper.ReadCsv(filePath);
        await _context.Movies.AddRangeAsync(movies);
        await _context.SaveChangesAsync();
    }
}

app.Run();

public partial class Program { }
