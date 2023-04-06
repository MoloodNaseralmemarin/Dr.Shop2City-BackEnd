using System.Text;
using DrShop2City.DataLayer.Repository;
using DrShop2City.Infrastructure.Security;
using DrShop2City.Infrastructure.Services.Implementations;
using DrShop2City.Infrastructure.Services.Interfaces;
using DrShop2City.Infrastructure.Utilities.Extensions.Connection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var webHostEnvironment = builder.Environment;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSingleton<IConfiguration>(
    new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile($"appsettings.json")
        .Build());

#region Add DbContext
builder.Services.AddApplicationDbContext(configuration);
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://apidr.shop2city.ir",
            ValidAudience = "https://apidr.shop2city.ir",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEshopJwtBearer"))
        };
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});
#endregion

#region CORS

builder.Services.AddCors(options => {
    options.AddPolicy("EnableCors",
        b => b.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod());
});

var app = builder.Build();
#endregion

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//    });
//}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("EnableCors");
app.UseAuthentication();
app.UseAuthorization();
    
app.MapControllers();
app.Run();
