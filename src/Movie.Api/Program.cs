using Microsoft.EntityFrameworkCore;
using Api.Database;
using Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MoviesDbContext>(options =>
    options.UseSqlite("DataSource=moviesDb"));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();

public partial class Program { }
