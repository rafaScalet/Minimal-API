using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain.DTOs;
using MinimalAPI.Domain.Interfaces;
using MinimalAPI.Domain.ModelViews;
using MinimalAPI.Domain.Services;
using MinimalAPI.Entities;
using MinimalAPI.Infrastructure.Db;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddScoped<IVehicleServices, VehicleServices>();

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

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region Admin
app.MapPost("/admin/login", ([FromBody] LoginDTO loginDTO, IAdminServices adminServices) => {
	if (adminServices.Login(loginDTO) != null)
		return Results.Ok("Login efetuado");
	else
		return Results.Unauthorized();
}).WithTags("Admin");
#endregion

#region Vehicles
app.MapPost("/vehicle", ([FromBody] VehicleDTO vehicleDTO, IVehicleServices vehicleServices) => {
	var vehicle = new Vehicles{
		Mark = vehicleDTO.mark,
		Name = vehicleDTO.name,
		Year = vehicleDTO.year,
	};

	vehicleServices.Save(vehicle);

	return Results.Created($"/vehicle/{vehicle.Id}", vehicle);
}).WithTags("Vehicles");

app.MapGet("/vehicles", ([FromQuery] int? page, IVehicleServices vehicleServices) => {
	var vehicles = vehicleServices.ListAll(page);

	return Results.Ok(vehicles);
}).WithTags("Vehicles");


#endregion

#region APP
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
#endregion