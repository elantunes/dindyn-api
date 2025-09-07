using Dindyn.App.Cliente.Repositories;
using Dindyn.App.Dtos;
using Dindyn.App.Models;
using Dindyn.App.Shared.Services;
using Dindyn.Commons.Exceptions;

namespace Dindyn.App.UseCases.Auth.GerarToken;

public class GerarTokenUseCase(
	IValidationService validationService,
	IClienteRepository clienteRepository) : IGerarTokenUseCase
{
	private readonly IValidationService _validationService = validationService;

	public async Task<Resposta> Execute(LoginRequest request)
	{
		var validationErrors = _validationService.Validate(request);

		if (validationErrors.Count != 0)
			return new Resposta(false, null, validationErrors);

		var token = await clienteRepository.GerarToken();

		if (token != null)
			return new Resposta(true, new { Token = "fake-token" });

		return new Resposta(false, null, [Erro.ClienteCredenciaisInvalidas]);
	}
}
