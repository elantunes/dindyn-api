using Dapper;
using Dindyn.App.Cliente.Repositories;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Dindyn.Infra;

public class ClienteRepository(IConfiguration configuration) : IClienteRepository
{
	private readonly IConfiguration _configuration = configuration;

	public bool Logon()
	{
		var connectionString = _configuration.GetConnectionString("Dindyn");

		var sql = "SELECT 1";
		
		using var conexao = new MySqlConnection(connectionString);

		var loginValido = conexao.Query<bool>(sql).FirstOrDefault();
		
		return loginValido;
	}
}
