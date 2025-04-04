using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Reunite.Services.Implementations;
using Reunite.Services.Interfaces;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Reunite.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ReuniteDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        NameClaimType = ClaimTypes.NameIdentifier,
        RoleClaimType = "https://my-app.example.com/roles"
    };
});
builder.Services.AddHttpClient<IAuthService, AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();