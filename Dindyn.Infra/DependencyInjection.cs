using Dindyn.App.Cliente;
using Dindyn.App.Cliente.Repositories;
using Dindyn.App.Cliente.Services;
using Dindyn.App.Services;
using Dindyn.Infra.Extensions;
using Dindyn.Infra.Factories;
using Dindyn.Infra.Repositories;
using Dindyn.Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dindyn.Infra;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		// Database Services
		services.AddScoped<IDbConnectionFactory>(provider => 
			new DbConnectionFactory(configuration.GetConnectionString("Dindyn")!));
		services.AddScoped<IDapperService, DapperService>();

		// Application Services
		services.AddScoped<IClienteApp, ClienteApp>();
		services.AddScoped<IValidationService, ValidationService>();
		services.AddScoped<IClienteService, ClienteService>();

		// Repositories
		services.AddScoped<IClienteRepository, ClienteRepository>();

		// Configurar FluentValidation
		services.AddFluentValidation();

		return services;
	}
}
