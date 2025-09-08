using Dindyn.App.Dtos;
using Dindyn.App.Interfaces;
using Dindyn.App.Models;
using Dindyn.App.Shared.Services;
using Dindyn.Commons.Exceptions;
using Dindyn.Commons.Extensions;
using Dindyn.Commons.Helpers;
using Dindyn.Domain.Entities;

namespace Dindyn.App.UseCases.Auth.GerarToken;

public class GerarTokenUseCase(
	IValidationService validationService,
	IDindynDbContext context) : IGerarTokenUseCase
{
	private readonly IValidationService _validationService = validationService;
	private readonly IDindynDbContext _context = context;

	public async Task<Resposta> Execute(LoginRequest request)
	{
		var validationErrors = _validationService.Validate(request);

		if (validationErrors.Count != 0)
			return new Resposta(validationErrors);

		// Busca o cliente

		var cliente = BuscarCliente(request.Email, request.Senha);

		if (cliente == null)
			return new Resposta(Erro.ClienteCredenciaisInvalidas);

		// Gera o token

		var token = StringHelper.TextoAleatorio(45);

		var tokenAcesso = new TokenAcesso
		{
			ClienteId = cliente.Id,
			Token = token,
			DataValidade = DateTime.UtcNow.AddYears(1)
		};

		_context.Tokens.Add(tokenAcesso);
		await _context.SaveChangesAsync();

		return new Resposta(true, token);
	}
	
	private Domain.Entities.Cliente? BuscarCliente(string email, string senha) =>
		_context
		.Clientes
		.FirstOrDefault(c =>
			c.Email == email &&
			c.Senha == senha.ParaSha256());

}
