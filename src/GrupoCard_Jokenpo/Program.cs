using GrupoCard_Jokenpo.Services;
using GrupoCard_Jokenpo.Hubs;
using GrupoCard_Jokenpo.Engine;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sw =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    sw.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IGame, Game>();
builder.Services.AddSingleton<IPlayers, Players>();
builder.Services.AddSignalR();
builder.Services.AddHostedService<GameHostedService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(c =>
{
    c.Cookie.Name = "Token";
    c.LoginPath = "/SignIn";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();

app.MapHub<PlayerHub>("/hubs/player");

app.UseAuthentication();

app.UseAuthorization();

app.Run();
