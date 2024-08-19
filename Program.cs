using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain.DTOs;
using MinimalAPI.Infrastructure.Db;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

connectionString = connectionString?.Replace("{DB_PASSWORD}", dbPassword);

builder.Services.AddDbContext<MyContext>(options => {
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/teste", () => builder);

app.MapPost("/login", (LoginDTO loginDTO) => {
	if (loginDTO.Email == "admin@email.com" && loginDTO.PWD == "1234")
		return Results.Ok("Login efetuado");
	else
		return Results.Unauthorized();
});

app.Run();