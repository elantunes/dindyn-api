using System.Security.Cryptography;
using System.Text;

namespace Dindyn.Commons.Helpers;

public static class StringHelper
{
	/// <summary>
	/// Retorna um texto aleatório com o conjunto de caracteres informados no parâmetro caracteresPermitidos
	/// </summary>
	/// <param name="tamanho">Número de caracteres do texto</param>
	/// <param name="caracteresPermitidos">Define o conjunto de caracteres que podem ser usados.</param>
	/// <returns>Retorna um texto aleatório</returns>
	public static string TextoAleatorio(int tamanho, string caracteresPermitidos)
	{
		// Cria um array de bytes para armazenar os números aleatórios.
		var dadosAleatorios = new byte[tamanho];

		// Preenche o array com bytes aleatórios seguros.
		RandomNumberGenerator.Fill(dadosAleatorios);

		// Cria a string a partir dos bytes.
		var sb = new StringBuilder(tamanho);
		foreach (byte b in dadosAleatorios)
			// O uso de módulo garante que o índice esteja dentro do
			// intervalo de caracteres permitidos.
			sb.Append(caracteresPermitidos[b % caracteresPermitidos.Length]);

		return sb.ToString();
	}

	/// <summary>
	/// Retorna um texto aleatório com o conjunto de caracteres abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789
	/// </summary>
	/// <param name="tamanho">Número de caracteres do texto</param>
	/// <returns>Retorna um texto aleatório</returns>
	public static string TextoAleatorio(int tamanho)
	{
		return TextoAleatorio(tamanho, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
	}
}
