using System.Text;
using AngleSharp;
using DrShop2City.DataLayer.Context;
using DrShop2City.DataLayer.Repository;
using DrShop2City.Infrastructure.Security;
using DrShop2City.Infrastructure.Services.Implementations;
using DrShop2City.Infrastructure.Services.Interfaces;
using DrShop2City.Infrastructure.Utilities.Extensions.Connection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


//Default application configuration sources

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment; 


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var connectionString = new ConfigurationBuilder()
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .Build();

#region DataBase Context 
builder.Services.AddDbContext<DrShop2CityDBContext>(options =>
{
    options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:DrShop2CityConnection"));
});
#endregion
#region Add DbContext

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
#endregion

#region Application Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAccessService, AccessService>();
#endregion

#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    
    o.TokenValidationParameters = new TokenValidationParameters
    {
       ValidIssuer = "https://apidr.shop2city.ir",
       IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes("AngularEshopJwtBearer")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins()
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                  
                      });
});
#endregion

var app = builder.Build();

#region wagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

#endregion
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();
