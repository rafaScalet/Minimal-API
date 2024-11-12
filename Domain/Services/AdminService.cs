using MinimalAPI.Domain.DTOs;
using MinimalAPI.Domain.Interfaces;
using MinimalAPI.Entities;
using MinimalAPI.Infrastructure.Db;

namespace MinimalAPI.Domain.Services;

public class AdminServices : IAdminServices
{
	private readonly MyContext _myContext;

	public AdminServices (MyContext myContext)
	{
		_myContext = myContext;
	}

	public List<Admin> List(int? page)
	{
		int itemsPerPage = 10;

		var query = _myContext.Admin.AsQueryable();

		if (page != null) {
			query = query.Skip(((int)page - 1) * itemsPerPage).Take(itemsPerPage);
		}

		return [.. query];
	}

	public Admin? Login (LoginDTO loginDTO)
	{
		return _myContext.Admin.Where(admin => admin.Email == loginDTO.Email && admin.PWD == loginDTO.PWD).FirstOrDefault();
	}

	public Admin Save(Admin admin)
	{
		_myContext.Add(admin);
		_myContext.SaveChanges();

		return admin;
	}
}