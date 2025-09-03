using Dindyn.App.Dtos;
using Dindyn.App.Models;

namespace Dindyn.App.Cliente;

public class ClienteApp : IClienteApp
{
	public Resposta Login(LoginRequest request)
	{
		var resposta = new Resposta(false);

		return resposta;
	}
}
