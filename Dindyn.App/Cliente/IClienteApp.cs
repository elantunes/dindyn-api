using Dindyn.App.Dtos;
using Dindyn.App.Models;

namespace Dindyn.App.Cliente;

public interface IClienteApp
{
    Resposta Logon(LoginRequest request);
}
