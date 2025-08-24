using Dindyn.Commons.Exceptions;

namespace Dindyn.App.Models;

public class Resposta
{
	public bool IsValid { get; set; }
	public object? Result { get; set; }
	public List<ErroInfo> Errors { get; set; } = [];

	public Resposta(bool isValid)
	{
		IsValid = isValid;
		Errors = [];
	}

	public Resposta(ErroInfo error)
	{
		IsValid = false;
		Errors = [error];
	}

	/// <summary>
	/// isValid = false; result = null.
	/// </summary>
	/// <param name="errors"></param>
	public Resposta(List<ErroInfo> errors)
	{
		IsValid = false;
		Errors = errors;
	}

	public Resposta(bool isValid, object result)
	{
		IsValid = isValid;
		Result = result;
		Errors = [];
	}

	public Resposta(bool isValid, object? result, List<ErroInfo> errors)
	{
		IsValid = isValid;
		Result = result;
		Errors = errors;
	}
}
