using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dindyn.Api.Filters;

public class CustomHeaderParameterFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		operation.Parameters =
		[
			new OpenApiParameter
			{
				Name = "X-Api-Key",
				In = ParameterLocation.Header,
				Description = "Header para autenticacao",
				Required = true
			},
		];
	}
}
