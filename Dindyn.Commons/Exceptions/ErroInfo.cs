namespace Dindyn.Commons.Exceptions;

public class ErroInfo (string codigo, string mensagem)
{
	public string Codigo { get; set; } = codigo;
	public string Mensagem { get; set; } = mensagem;
	public ErroInfo(string mensagem) : this(string.Empty, mensagem) { }
}
