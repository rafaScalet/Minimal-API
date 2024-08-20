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

	public Admin? Login (LoginDTO loginDTO)
	{
		return _myContext.Admin.Where(admin => admin.Email == loginDTO.Email && admin.PWD == loginDTO.PWD).FirstOrDefault();
	}
}