using MinimalAPI.Domain.DTOs;
using MinimalAPI.Entities;

namespace MinimalAPI.Domain.Interfaces;

public interface IAdminServices
{
	Admin? Login (LoginDTO loginDTO);
	Admin Save (Admin admin);
	List<Admin> List (int? page);
}