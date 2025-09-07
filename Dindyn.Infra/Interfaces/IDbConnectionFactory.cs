using System.Data;

namespace Dindyn.Infra.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
