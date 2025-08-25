namespace Dindyn.Commons.Exceptions;

public static class Erro
{
	public static readonly ErroInfo SistemaGenerico = new("SYS00", "Ocorreu um erro inesperado.");
	public static readonly ErroInfo SistemaChaveSegurancaAustente = new("SYS01", "Chave de segurança ausente.");
	public static readonly ErroInfo SsitemaChaveDeSegurancaInvalida = new("SYS02", "Chave de segurança inválida.");
}
