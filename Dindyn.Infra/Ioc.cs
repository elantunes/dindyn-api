using Dindyn.App.Cliente;
using Microsoft.Extensions.DependencyInjection;

namespace Dindyn.Infra;

public static class Ioc
{
	public static void AddIDependencyInjection(IServiceCollection services)
	{
		services.AddScoped<IClienteApp, ClienteApp>();
	}
}
