using Dindyn.App.Cliente.Repositories;
using Dindyn.App.Dtos;

namespace Dindyn.App.Cliente.Services;

public class ClienteService(
	IClienteRepository clienteRepository) : IClienteService
{
	public async Task<bool> Logon(LoginRequest request)
	{
		var logon = await clienteRepository.Logon();
		return logon;
	}
}
