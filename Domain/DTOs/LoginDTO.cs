namespace MinimalAPI.Domain.DTOs;

public class LoginDTO
{
	public string Email { get; set; } = default!;
	public string PWD { get; set; } = default!;
}