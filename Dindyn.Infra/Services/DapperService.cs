using Dapper;
using Dindyn.Infra.Factories;

namespace Dindyn.Infra.Services;

public class DapperService(IDbConnectionFactory connectionFactory) : IDapperService
{
	private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

	public async Task<T?> QueryFirstOrDefault<T>(string sql, object? parameters = null)
	{
		using var connection = _connectionFactory.CreateConnection();
		return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
	}
}
