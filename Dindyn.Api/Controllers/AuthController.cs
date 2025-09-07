using Dindyn.App.Cliente;
using Dindyn.App.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Dindyn.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IClienteApp clienteApp) : ControllerBase
{
	private readonly IClienteApp _clienteApp = clienteApp;

	[HttpPost("logon")]
	public IActionResult Logon([FromBody] LoginRequest request)
	{
		var resposta = _clienteApp.Logon(request);

		return Ok(resposta);
	}
}

