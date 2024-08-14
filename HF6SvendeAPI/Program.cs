using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Application.Services;
using HF6Svende.Core.Interfaces;
using HF6Svende.Infrastructure.Repository;
using HF6SvendeAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HF6Svende.Application.Mappings;
using HF6Svende.Core.Repository_Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Get connection string
builder.Services.AddDbContext<DemmacsWatchesDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("localhost")));

// Add references
//Listings
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<IListingService, ListingService>();
//Products
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Add services to the container.
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
