using Dindyn.App.Models;
using Dindyn.Commons.Exceptions.Sistema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dindyn.Api;

public class ApiExceptionFilter : IExceptionFilter
{
	public void OnException(ExceptionContext context)
	{
		var statusCode = 500;
		object response;

		//if (context.Exception is Exception)
		//{
		// Exceção genérica
		var erro = ErroGenericoException.ErroInfo;
		response = new Resposta(erro);
		//}

		context.Result = new ObjectResult(response)
		{
			StatusCode = statusCode
		};

		context.ExceptionHandled = true;
	}
}