using Dindyn.App.Cliente;
using Dindyn.App.Cliente.Repositories;
using Dindyn.App.Cliente.Services;
using Dindyn.App.Services;
using Dindyn.Infra.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Dindyn.Infra;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddScoped<IClienteApp, ClienteApp>();

		services.AddScoped<IValidationService, ValidationService>();
		services.AddScoped<IClienteService, ClienteService>();

		services.AddScoped<IClienteRepository, ClienteRepository>();

		// Configurar FluentValidation
		services.AddFluentValidation();

		return services;
	}
}
