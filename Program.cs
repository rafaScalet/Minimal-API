var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", (LoginDTO loginDTO) => {
	if (loginDTO.Email == "admin@email.com" && loginDTO.PWD == "1234")
		return Results.Ok("Login efetuado");
	else
		return Results.Unauthorized();
});

app.Run();

public class LoginDTO
{
	public string Email { get; set; } = default!;
	public string PWD { get; set; } = default!;
}