using Microsoft.EntityFrameworkCore;
using Portal_Api.Models;
using Portal_Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EBookMetaDataDbContext>(options => options.UseSqlite("Data Source = EBookMetaData.db"));
builder.Services.AddScoped<IEBookMetaDataService, EBookMetaDataService>();
builder.Services.AddScoped<IEpubReaderService, EpubReaderService>();

builder.Services.AddControllers();
builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Middlewares
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

app.UseCors(
    options => options.WithOrigins("http://127.0.0.1:5173").AllowAnyMethod().AllowAnyHeader()
);

app.Run();
