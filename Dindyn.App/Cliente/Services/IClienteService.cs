using Dindyn.App.Dtos;

namespace Dindyn.App.Cliente.Services;

public interface IClienteService
{
	public bool Logon(LoginRequest request);
}
