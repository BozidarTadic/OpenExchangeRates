using Microsoft.EntityFrameworkCore;
using OpenExchangeRates.Api.BL.Interfaces;
using OpenExchangeRates.Api.BL.Services;
using OpenExchangeRates.Api.Data;

var builder = WebApplication.CreateBuilder(args);
//add database 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//add http client 

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://openexchangerates.org/api/") });

// Add services to the container.
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
