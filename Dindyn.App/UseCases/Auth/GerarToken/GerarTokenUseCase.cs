using Dindyn.App.Dtos;
using Dindyn.App.Interfaces;
using Dindyn.App.Models;
using Dindyn.App.Shared.Services;
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
			return new Resposta(false, null, validationErrors);
		
		var tokenValue = Guid.NewGuid().ToString("N");

		var token = new TokenAcesso
		{
			ClienteId = 1,
			Token = tokenValue
		};

		_context.Tokens.Add(token);
		await _context.SaveChangesAsync();

		return new Resposta(true, new { 
			Token = tokenValue,
			ClienteId = 1
		});
	}
}
