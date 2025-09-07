namespace Dindyn.App.Cliente.Repositories;

public interface IClienteRepository
{
	public Task<bool> Logon();
}
