using Microsoft.EntityFrameworkCore;
using Api.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MoviesDbContext>(options =>
    options.UseSqlite("DataSource=moviesDb"));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();

public partial class Program { }
