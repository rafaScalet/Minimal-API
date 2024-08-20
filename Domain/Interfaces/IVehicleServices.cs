
using MinimalAPI.Entities;

namespace MinimalAPI.Domain.Interfaces;

public interface IVehicleServices
{
	List<Vehicles> ListAll (int page = 1, string? name = null, string? mark = null);
	Vehicles? SearchForId (int id);
	void Update (Vehicles vehicles);
	void Save (Vehicles vehicles);
	void Delete (Vehicles vehicles);
}