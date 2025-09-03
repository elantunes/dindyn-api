using Dindyn.Commons.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dindyn.App.Services;

public class ValidationService(IServiceProvider serviceProvider) : IValidationService
{
	private readonly IServiceProvider _serviceProvider = serviceProvider;

	public List<ErroInfo> Validate<T>(T model)
	{
		var validator = _serviceProvider.GetService<IValidator<T>>();

		if (validator is null)
			return [];

		var result = validator.Validate(model);

		var resultado = result.Errors.Select(CreateErroInfoFromErrorCode).ToList();

		return resultado;
	}

	private static ErroInfo CreateErroInfoFromErrorCode(ValidationFailure error)
	{
		// Usar reflection para encontrar o ErroInfo pelo código
		var erroType = typeof(Erro);
		var fields = erroType.GetFields(BindingFlags.Public | BindingFlags.Static);

		foreach (var field in fields)
			if (field.GetValue(null) is ErroInfo erroInfo && erroInfo.Codigo == error.ErrorCode)
				return erroInfo;

		// Se não encontrar, criar um genérico
		return new ErroInfo(error.ErrorCode, error.ErrorMessage);
	}

	public bool IsValid<T>(T model)
	{
		var errors = Validate(model);
		return errors.Count == 0;
	}
}
