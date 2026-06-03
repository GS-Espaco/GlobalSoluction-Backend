using GlobalSoluction.Data;
using GlobalSoluction.Interfaces;
using GlobalSoluction.Repositories;
using GlobalSoluction.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException(
            "A connection string 'DefaultConnection' não foi encontrada.");
    }

    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IEstufaRepository, EstufaRepository>();
builder.Services.AddScoped<IEstufaService, EstufaService>();

builder.Services.AddScoped<ILocalOrbitalRepository, LocalOrbitalRepository>();
builder.Services.AddScoped<ILocalOrbitalService, LocalOrbitalService>();

builder.Services.AddScoped<ILeituraSensorRepository, LeituraSensorRepository>();
builder.Services.AddScoped<ILeituraSensorService, LeituraSensorService>();

builder.Services.AddScoped<IAlertaEstufaRepository, AlertaEstufaRepository>();
builder.Services.AddScoped<IAlertaEstufaService, AlertaEstufaService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirMobile", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var jwtKey = builder.Configuration["Jwt:Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException(
        "A chave JWT não foi configurada.");
}

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
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GlobalSoluction API",
        Version = "v1",
        Description = "API do projeto GlobalSoluction - Agricultura Espacial"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "GlobalSoluction API v1");
    });
}

app.UseHttpsRedirection();

app.UseCors("PermitirMobile");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();