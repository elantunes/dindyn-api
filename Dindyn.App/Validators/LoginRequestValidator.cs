using Dindyn.App.Dtos;
using FluentValidation;

namespace Dindyn.App.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
	public LoginRequestValidator()
	{
		RuleFor(x => x.Email)
			.EmailObrigatorio()
			.EmailFormatoValido()
			.EmailTamanhoMaximo();

		RuleFor(x => x.Senha)
			.SenhaObrigatoria()
			.SenhaTamanhoMinimo()
			.SenhaTamanhoMaximo();
	}
}
