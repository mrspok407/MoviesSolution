using Microsoft.EntityFrameworkCore;
using Movies;
using Movies.Data;
using Movies.Interfaces;
using Movies.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRespository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
