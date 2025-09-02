using Dindyn.App.Models;
using Dindyn.Commons.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dindyn.Api.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ChaveSegurancaFilter: Attribute, IActionFilter
{
	public void OnActionExecuting(ActionExecutingContext context)
	{
		var configurationObj = context.HttpContext.RequestServices.GetService(typeof(IConfiguration));

		if (configurationObj is not IConfiguration configuration)
			throw new Exception("Erro ao obter IConfiguration");

		var chave = configuration["ChaveSeguranca"];

		var temXApiKey = context.HttpContext.Request.Headers.ContainsKey("X-Api-Key");

		if (!temXApiKey)
		{
			var resposta = new Resposta(Erro.SistemaChaveSegurancaAustente);
			context.Result = new JsonResult(resposta) { StatusCode = 401 };
		}
		else
		{
			var chaveFornecida = context.HttpContext.Request.Headers["X-Api-Key"].ToString();

			if (chaveFornecida != chave)
			{
				var resposta = new Resposta(Erro.SsitemaChaveDeSegurancaInvalida);
				context.Result = new JsonResult(resposta) { StatusCode = 401 };
			}
		}
	}

	public void OnActionExecuted(ActionExecutedContext context)
	{
		// Não faz nada após a execução
	}
}

