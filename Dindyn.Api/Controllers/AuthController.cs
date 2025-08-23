using Dindyn.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dindyn.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
	[HttpPost("login")]
	public IActionResult Login([FromBody] LoginRequest request)
	{
		var resposta = new Resposta<object>
		{
			IsValid = true,
			Result = null,
			Errors = []
		};
		return Ok(resposta);
	}
}

public class LoginRequest
{
	public required string Email { get; set; }
	public required string Password { get; set; }
}
