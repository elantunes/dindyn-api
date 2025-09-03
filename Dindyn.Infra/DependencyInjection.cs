using Dindyn.App.Cliente;
using Microsoft.Extensions.DependencyInjection;

namespace Dindyn.Infra;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddScoped<IClienteApp, ClienteApp>();

		return services;
	}
}
