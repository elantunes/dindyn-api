namespace Dindyn.Infra.Services;

public interface IDapperService
{
	Task<T?> QueryFirstOrDefault<T>(string sql, object? parameters = null);
}
