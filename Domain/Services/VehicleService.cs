using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain.Interfaces;
using MinimalAPI.Entities;
using MinimalAPI.Infrastructure.Db;

namespace MinimalAPI.Domain.Services;

public class VehicleServices(MyContext myContext) : IVehicleServices
{
	private readonly MyContext _myContext = myContext;

	public void Delete(Vehicles vehicles)
	{
		_myContext.Vehicles.Remove(vehicles);
		_myContext.SaveChanges();
	}

	public List<Vehicles> ListAll(int? page = 1, string? name = null, string? mark = null)
	{
		int itemsPerPage = 10;

		var query = _myContext.Vehicles.AsQueryable();

		if (!string.IsNullOrEmpty(name))
		{
			query = query.Where(v => EF.Functions.Like(v.Name.ToLower(), $"%{name.ToLower()}%"));
		}

		if (page != null) {
			query = query.Skip(((int)page - 1) * itemsPerPage).Take(itemsPerPage);
		}

		return [.. query];
	}

	public void Save(Vehicles vehicles)
	{
		_myContext.Vehicles.Add(vehicles);
		_myContext.SaveChanges();
	}

	public Vehicles? SearchForId(int id)
	{
		return _myContext.Vehicles.Where(v => v.Id == id).FirstOrDefault();
	}

	public void Update(Vehicles vehicles)
	{
		_myContext.Vehicles.Update(vehicles);
		_myContext.SaveChanges();
	}
}