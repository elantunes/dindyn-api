using Dindyn.App.Dtos;

namespace Dindyn.App.Cliente.Services;

public interface IClienteService
{
	public Task<bool> Logon(LoginRequest request);
}
