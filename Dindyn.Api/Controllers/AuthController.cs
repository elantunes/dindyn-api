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
	public async Task<IActionResult> Logon([FromBody] LoginRequest request)
	{
		var resposta = await _clienteApp.Logon(request);

		return Ok(resposta);
	}
}

