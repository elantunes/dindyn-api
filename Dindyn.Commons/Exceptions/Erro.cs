namespace Dindyn.Commons.Exceptions;

public static class Erro
{
	public static readonly ErroInfo ClienteCredenciaisInvalidas = new("AUTH00", "Credenciais inválidas");
	public static readonly ErroInfo EmailObrigatorio = new("EMA00", "O e-mail é obrigatório.");
	public static readonly ErroInfo EmailFormatoInvalido = new("EMA01", "O e-mail está em formato incorreto.");
	public static readonly ErroInfo EmailTamanhoMaximo = new("EMA02", "O e-mail não pode ter mais de 254 caracteres.");
	public static readonly ErroInfo SenhaObrigatoria = new("SEN00", "A senha é obrigatória.");
	public static readonly ErroInfo SenhaTamanhoMinimo = new("SEN01", "A senha não pode ter menos de 6 caracteres.");
	public static readonly ErroInfo SenhaTamanhoMaximo = new("SEN02", "A senha não pode ter mais de 50 caracteres.");
	public static readonly ErroInfo SistemaGenerico = new("SYS00", "Ocorreu um erro inesperado.");
	public static readonly ErroInfo SistemaChaveSegurancaAustente = new("SYS01", "Chave de segurança ausente.");
	public static readonly ErroInfo SistemaChaveDeSegurancaInvalida = new("SYS02", "Chave de segurança inválida.");
}
