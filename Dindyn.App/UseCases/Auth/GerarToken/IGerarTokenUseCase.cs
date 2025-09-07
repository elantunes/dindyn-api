using Dindyn.App.Dtos;
using Dindyn.App.Models;

namespace Dindyn.App.UseCases.Auth.GerarToken;

public interface IGerarTokenUseCase
{
	Task<Resposta> Execute(LoginRequest request);
}
