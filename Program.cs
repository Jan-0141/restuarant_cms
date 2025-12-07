using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


using FOODCMS.API.Data;
using FOODCMS.API.Helpers;
using FOODCMS.API.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) Database: PostgreSQL
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("RestaurantDb")));

// 2) DI services
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<JwtTokenHelper>();

// 3) Controllers
builder.Services.AddControllers();

// 3.1) Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4) JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

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
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

// ✅ ให้ UseSwagger เฉพาะตอน Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// ถ้าอยากให้เข้า / แล้วเด้งไป swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();