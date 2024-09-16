using Microsoft.EntityFrameworkCore;
using MilkWala.IRepository;
using MilkWala.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MilkWala.Repositories;
using MilkWala.Repository;
using System.Text;
//using static JwtMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add JWT settings
//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));



// Add services to the container.
builder.Services.AddDbContext<MWDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IDeliveryBoysRepository, DeliveryBoyRepository>();
builder.Services.AddScoped<IOwnerRespository, OwnerRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add services to the container
builder.Services.AddControllers();


// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy to allow requests from the Angular app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        corsBuilder =>
        {
            corsBuilder.AllowAnyOrigin() // Angular app URL
                       .AllowAnyHeader()
                       .AllowAnyMethod();// If you're using cookies/authentication
        });
});

var app = builder.Build();

// Use the CORS policy
app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
