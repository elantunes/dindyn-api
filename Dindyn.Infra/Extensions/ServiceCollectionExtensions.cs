using Dindyn.App.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Dindyn.Infra.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddFluentValidation(this IServiceCollection services)
	{
		services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
		
		return services;
	}
}
