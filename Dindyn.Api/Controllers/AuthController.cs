using Dindyn.App.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace Dindyn.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
	[HttpPost("login")]
	public IActionResult Login([FromBody] LoginRequest request)
	{
		var resposta = ClienteApp.Login();

		return Ok(resposta);
	}
}

public class LoginRequest
{
	public required string Email { get; set; }
	public required string Password { get; set; }
}
