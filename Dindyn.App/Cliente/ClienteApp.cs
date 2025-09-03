using Dindyn.App.Dtos;
using Dindyn.App.Models;
using Dindyn.App.Services;
using Dindyn.Commons.Exceptions;

namespace Dindyn.App.Cliente;

public class ClienteApp(IValidationService validationService) : IClienteApp
{
	private readonly IValidationService _validationService = validationService;

	public Resposta Login(LoginRequest request)
	{
		var validationErrors = _validationService.Validate(request);

		if (validationErrors.Count != 0)
			return new Resposta(false, null, validationErrors);

		if (request.Email == "admin@dindyn.com" && request.Senha == "123456")
			return new Resposta(true, new { Token = "fake-token" });

		return new Resposta(false, null, [Erro.ClienteCredenciaisInvalidas]);
	}
}
