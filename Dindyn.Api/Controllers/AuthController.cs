using Dindyn.App.Cliente;
using Dindyn.App.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Dindyn.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IClienteApp clienteApp) : ControllerBase
{
	private readonly IClienteApp _clienteApp = clienteApp;

	[HttpPost("login")]
	public IActionResult Login([FromBody] LoginRequest request)
	{
		var resposta = _clienteApp.Login(request);

		return Ok(resposta);
	}
}

