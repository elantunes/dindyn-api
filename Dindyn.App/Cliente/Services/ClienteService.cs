using Dindyn.App.Cliente.Repositories;
using Dindyn.App.Dtos;

namespace Dindyn.App.Cliente.Services;

public class ClienteService(
	IClienteRepository clienteRepository) : IClienteService
{
	public bool Logon(LoginRequest request)
	{
		var logon = clienteRepository.Logon();
		return logon;
	}
}
