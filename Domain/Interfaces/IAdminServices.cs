using MinimalAPI.Domain.DTOs;
using MinimalAPI.Entities.Admin;

namespace MinimalAPI.Domain.Interfaces;

public interface IAdminServices
{
	Admin? Login (LoginDTO loginDTO);
}