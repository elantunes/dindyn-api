using System.Data;

namespace Dindyn.Infra.Factories;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
