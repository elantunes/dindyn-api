using System.Data;
using Dindyn.Infra.Interfaces;
using MySqlConnector;

namespace Dindyn.Infra;

public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = connectionString;

	public IDbConnection CreateConnection()
    {
        var conn = new MySqlConnection(_connectionString);
        conn.Open();
        return conn;
    }
}
