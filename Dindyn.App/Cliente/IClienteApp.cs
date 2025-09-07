using Dindyn.App.Dtos;
using Dindyn.App.Models;

namespace Dindyn.App.Cliente;

public interface IClienteApp
{
    Task<Resposta> Logon(LoginRequest request);
}
