using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Application.Services;
using HF6Svende.Core.Interfaces;
using HF6Svende.Infrastructure.Repository;
using HF6SvendeAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HF6Svende.Application.Mappings;
using HF6Svende.Core.Repository_Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Get connection string
builder.Services.AddDbContext<DemmacsWatchesDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DemmacsWatches")));

//var clientId = builder.Configuration["AzureAd:ClientId"];
//var clientSecret = builder.Configuration["AzureAd:ClientSecret"];
//var tenantId = builder.Configuration["AzureAd:TenantId"];

//builder.Services.AddScoped<ITokenService>(provider =>
//    new TokenService(clientId, clientSecret, tenantId));

// Add services to the container.
builder.Services.AddControllers();
// Add references
// Listings
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<IListingService, ListingService>();
// Products
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
// Customers
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
// Postalcodes
builder.Services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();
// Countries
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();
// Employees
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
// Logins
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
// Roles
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
// Images
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
// ProductColors
builder.Services.AddScoped<IProductColorRepository, ProductColorRepository>();
// Colors
builder.Services.AddScoped<IColorRepository, ColorRepository>();
// Orders
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = $"{builder.Configuration["AzureAd:Instance"]}{builder.Configuration["AzureAd:TenantId"]}";
//        options.Audience = builder.Configuration["AzureAd:Audience"];
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["AzureAd:Instance"] + builder.Configuration["AzureAd:TenantId"],
//            ValidAudience = builder.Configuration["AzureAd:Audience"]
//        };
//    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Dispatch API V1");
        s.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
