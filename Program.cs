using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain.DTOs;
using MinimalAPI.Domain.Interfaces;
using MinimalAPI.Domain.ModelViews;
using MinimalAPI.Domain.Services;
using MinimalAPI.Infrastructure.Db;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdminServices, AdminServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Env.Load();

var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

connectionString = connectionString?.Replace("{DB_PASSWORD}", dbPassword);

builder.Services.AddDbContext<MyContext>(options => {
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();
#endregion

#region Admin
app.MapGet("/", () => Results.Json(new Home()));

app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdminServices adminServices) => {
	if (adminServices.Login(loginDTO) != null)
		return Results.Ok("Login efetuado");
	else
		return Results.Unauthorized();
});
#endregion

#region Vehicles

#endregion

#region APP
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
#endregion