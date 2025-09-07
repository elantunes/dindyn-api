namespace Dindyn.App.Cliente.Repositories;

public interface IClienteRepository
{
	public Task<string> GerarToken();
}
