using Dindyn.App.Cliente.Services;
using Dindyn.App.Dtos;
using Dindyn.App.Models;
using Dindyn.App.Services;
using Dindyn.Commons.Exceptions;

namespace Dindyn.App.Cliente;

public class ClienteApp(
	IValidationService validationService,
	IClienteService clienteService) : IClienteApp
{
	private readonly IValidationService _validationService = validationService;
	private readonly IClienteService _clienteService = clienteService;

	public async Task<Resposta> Logon(LoginRequest request)
	{
		var validationErrors = _validationService.Validate(request);

		if (validationErrors.Count != 0)
			return new Resposta(false, null, validationErrors);

		var logonValido = await _clienteService.Logon(request);

		if (logonValido)
			return new Resposta(true,  new { Token = "fake-token" });

		return new Resposta(false, null, [Erro.ClienteCredenciaisInvalidas]);
	}
}
