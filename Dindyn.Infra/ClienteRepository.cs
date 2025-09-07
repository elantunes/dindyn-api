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
		using var comando = new MySqlCommand(sql, conexao);
		
		conexao.Open();
		var reader = comando.ExecuteReader();
		
		if (reader.Read())
		{
			var resultado = reader.GetInt32(0);
			if (resultado == 1)
				return true;
		}

		return true;
	}
}
