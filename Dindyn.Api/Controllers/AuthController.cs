using Dindyn.App.Dtos;
using Dindyn.App.UseCases.Auth.GerarToken;
using Microsoft.AspNetCore.Mvc;

namespace Dindyn.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IGerarTokenUseCase gerarTokenUseCase) : ControllerBase
{
	private readonly IGerarTokenUseCase _gerarTokenUseCase = gerarTokenUseCase;

	[HttpPost("logon")]
	public async Task<IActionResult> Logon([FromBody] LoginRequest request)
	{
		var resposta = await _gerarTokenUseCase.Execute(request);

		return Ok(resposta);
	}
}
