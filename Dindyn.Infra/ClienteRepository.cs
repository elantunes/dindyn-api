using Dapper;
using Dindyn.App.Cliente.Repositories;
using Dindyn.Infra.Interfaces;

namespace Dindyn.Infra;

public class ClienteRepository(IDbConnectionFactory factory) : IClienteRepository
{
	private readonly IDbConnectionFactory _factory = factory;

	public bool Logon()
	{
		var sql = "SELECT 1";

		using var conexao = _factory.CreateConnection();

		var loginValido = conexao.Query<bool>(sql).FirstOrDefault();

		return loginValido;
	}
}
