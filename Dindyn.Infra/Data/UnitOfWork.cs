using Dindyn.Infra.Factories;
using System.Data;

namespace Dindyn.Infra.Data;

public class UnitOfWork(IDbConnectionFactory connectionFactory) : IUnitOfWork
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
    
    public IDbConnection Connection { get; private set; } = null!;
    public IDbTransaction? Transaction { get; private set; }

    public void BeginTransaction()
    {
        Connection ??= _connectionFactory.CreateConnection();
        
        Transaction ??= Connection.BeginTransaction();
    }

    public void Commit()
    {
        Transaction?.Commit();
        DisposeTransaction();
    }

    public void Rollback()
    {
        Transaction?.Rollback();
        DisposeTransaction();
    }

    private void DisposeTransaction()
    {
        Transaction?.Dispose();
        Transaction = null;
    }

    public void Dispose()
    {
        Transaction?.Dispose();
        Connection?.Dispose();
        GC.SuppressFinalize(this);
    }
}
