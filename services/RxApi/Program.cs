using Microsoft.EntityFrameworkCore;
using RxApi;

//NpgsqlConnection.GlobalTypeMapper.UseNodaTime();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument();

builder.Services.AddDbContext<AstroContext>(options =>
    options.UseNpgsql("Host=host.docker.internal;Database=astro;Username=postgres;Password=aleksa")); // builder.Configuration.GetConnectionString()

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
