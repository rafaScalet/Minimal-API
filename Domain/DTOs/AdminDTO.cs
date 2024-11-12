using MinimalAPI.Domain.Enums;

namespace MinimalAPI.Domain.DTOs;

public class AdminDTO
{
	public string Email { get; set; } = default!;
	public string PWD { get; set; } = default!;
	public Profile? Profile { get; set; } = default!;
}