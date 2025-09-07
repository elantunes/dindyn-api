using System.Data;

namespace Dindyn.Infra.Data;

public interface IUnitOfWork : IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction? Transaction { get; }
    
    void BeginTransaction();
    void Commit();
    void Rollback();
}
