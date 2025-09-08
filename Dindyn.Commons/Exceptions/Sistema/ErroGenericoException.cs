namespace Dindyn.Commons.Exceptions.Sistema;

public class ErroGenericoException : Exception
{
	public static ErroInfo ErroInfo
	{
		get
		{
			return Erro.SistemaGenerico;
		}
	}

	public ErroGenericoException()
		: base(ErroInfo.Mensagem) {}
}
