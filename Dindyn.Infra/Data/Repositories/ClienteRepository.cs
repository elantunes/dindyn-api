using Dindyn.App.Cliente.Repositories;
using Dindyn.Infra.Services;

namespace Dindyn.Infra.Data.Repositories;

public class ClienteRepository(IDapperService dapperService) : IClienteRepository
{
	private readonly IDapperService _dapperService = dapperService;

	public async Task<bool> Logon()
	{
		var sql = "SELECT 1";
		var loginValido = await _dapperService.QueryFirstOrDefault<bool>(sql);
		return loginValido;
	}
}
