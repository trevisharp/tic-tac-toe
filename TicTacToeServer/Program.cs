using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using TicTacToeServer;
using TicTacToeServer.Entities;
using TicTacToeServer.Services.Token;
using TicTacToeServer.Services.Player;
using TicTacToeServer.Services.Matches;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = "tic-tac-toe",
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = builder.Configuration.GetJwtSecurityKey(),
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options => {
    options.AddPolicy("MainPolicy", builder => {
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

builder.Services
    .AddSingleton(builder.Configuration)
    .AddTransient<ITokenService, JWTTokenService>()
    .AddScoped<IPlayerService, EFPlayerService>()
    .AddScoped<IMatchesService, EFMatchesService>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<TicTacToeDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("MainPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();