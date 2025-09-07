using Dindyn.App.Cliente.Repositories;
using Dindyn.App.Shared.Services;
using Dindyn.App.UseCases.Auth.GerarToken;
using Dindyn.Infra.Data;
using Dindyn.Infra.Data.Repositories;
using Dindyn.Infra.Extensions;
using Dindyn.Infra.Factories;
using Dindyn.Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dindyn.Infra.Ioc;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		// Database Services
		services.AddScoped<IDapperService, DapperService>();
		services.AddScoped<IDbConnectionFactory>(provider => new DbConnectionFactory(configuration.GetConnectionString("Dindyn")!));
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		// Application Services
		services.AddScoped<IValidationService, ValidationService>();

		// Use Cases
		services.AddScoped<IGerarTokenUseCase, GerarTokenUseCase>();

		// Repositories
		services.AddScoped<IClienteRepository, ClienteRepository>();

		// Configurar FluentValidation
		services.AddFluentValidation();

		return services;
	}
}
