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
builder.Services.AddScoped<IValidationServices, ValidationServices>();

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

app.MapPost("/admin", ([FromBody] AdminDTO adminDTO, IValidationServices validationServices, IAdminServices adminServices) => {
	var validationList = validationServices.Validation(adminDTO);

	if (validationList.Messages.Count > 0) return Results.BadRequest(validationList);

	var admin = new Admin {
		Email = adminDTO.Email,
		PWD = adminDTO.PWD,
		#pragma warning disable CS8601 // Possible null reference assignment.
		Profile = adminDTO.Profile.ToString(),
		#pragma warning restore CS8601 // Possible null reference assignment.
	};

	adminServices.Save(admin);

	return Results.Created();
}).WithTags("Admin");

app.MapGet("/admins", ([FromQuery] int? page, IAdminServices adminServices) => {
	var admins = adminServices.List(page);

	return Results.Ok(admins);
}).WithTags("Admin");
#endregion

#region Vehicles
app.MapPost("/vehicle", ([FromBody] VehicleDTO vehicleDTO, IValidationServices validationServices, IVehicleServices vehicleServices) => {
	var validationList = validationServices.Validation(vehicleDTO);

	if (validationList.Messages.Count > 0) return Results.BadRequest(validationList);

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

app.MapGet("/vehicle/{id}", ([FromRoute] int id, IVehicleServices vehicleServices) => {
	var vehicle = vehicleServices.SearchForId(id);

	if (vehicle == null) return Results.NotFound();

	return Results.Ok(vehicle);
}).WithTags("Vehicles");

app.MapPut("/vehicle/{id}", ([FromRoute] int id, VehicleDTO vehicleDTO, IValidationServices validationServices, IVehicleServices vehicleServices) => {
	var vehicle = vehicleServices.SearchForId(id);

	if (vehicle == null) return Results.NotFound();

	var validationList = validationServices.Validation(vehicleDTO);

	if (validationList.Messages.Count > 0) return Results.BadRequest(validationList);

	vehicle.Name = vehicleDTO.name;
	vehicle.Mark = vehicleDTO.mark;
	vehicle.Year = vehicleDTO.year;

	vehicleServices.Update(vehicle);

	return Results.Ok(vehicle);
}).WithTags("Vehicles");

app.MapDelete("/vehicle/{id}", ([FromRoute] int id, IVehicleServices vehicleServices) => {
	var vehicle = vehicleServices.SearchForId(id);

	if (vehicle == null) return Results.NotFound();

	vehicleServices.Delete(vehicle);

	return Results.NoContent();
}).WithTags("Vehicles");
#endregion

#region APP
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
#endregion