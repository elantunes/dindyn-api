using Dindyn.Commons.Exceptions;
using FluentValidation;

namespace Dindyn.App.Validators;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, string> EmailObrigatorio<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithErrorCode(Erro.EmailObrigatorio.Codigo)
            .WithMessage(Erro.EmailObrigatorio.Mensagem);
    }

    public static IRuleBuilderOptions<T, string> EmailFormatoValido<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .EmailAddress()
            .WithErrorCode(Erro.EmailFormatoInvalido.Codigo)
            .WithMessage(Erro.EmailFormatoInvalido.Mensagem);
    }

    public static IRuleBuilderOptions<T, string> EmailTamanhoMaximo<T>(this IRuleBuilder<T, string> ruleBuilder, int maxLength = 254)
    {
        return ruleBuilder
            .MaximumLength(maxLength)
            .WithErrorCode(Erro.EmailTamanhoMaximo.Codigo)
            .WithMessage(Erro.EmailTamanhoMaximo.Mensagem);
    }

    public static IRuleBuilderOptions<T, string> SenhaObrigatoria<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithErrorCode(Erro.SenhaObrigatoria.Codigo)
            .WithMessage(Erro.SenhaObrigatoria.Mensagem);
    }

    public static IRuleBuilderOptions<T, string> SenhaTamanhoMinimo<T>(this IRuleBuilder<T, string> ruleBuilder, int minLength = 6)
    {
        return ruleBuilder
            .MinimumLength(minLength)
            .WithErrorCode(Erro.SenhaTamanhoMinimo.Codigo)
            .WithMessage(Erro.SenhaTamanhoMinimo.Mensagem);
    }

    public static IRuleBuilderOptions<T, string> SenhaTamanhoMaximo<T>(this IRuleBuilder<T, string> ruleBuilder, int maxLength = 50)
    {
        return ruleBuilder
            .MaximumLength(maxLength)
            .WithErrorCode(Erro.SenhaTamanhoMaximo.Codigo)
            .WithMessage(Erro.SenhaTamanhoMaximo.Mensagem);
    }
}