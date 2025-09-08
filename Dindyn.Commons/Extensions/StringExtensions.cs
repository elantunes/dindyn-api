using System.Security.Cryptography;
using System.Text;

namespace Dindyn.Commons.Extensions;

public static class StringExtensions
{
	/// <summary>
	/// Tramsforma o texto em um hash SHA-256
	/// </summary>
	/// <param name="texto">Texto a ser convertido</param>
	/// <returns>Um texto SHA-256</returns>
	public static string ParaSha256(this string texto)
	{
		if (string.IsNullOrEmpty(texto))
			return string.Empty;

		var bytes = Encoding.UTF8.GetBytes(texto);
		var hashBytes = SHA256.HashData(bytes);

		var sb = new StringBuilder();
		for (var i = 0; i < hashBytes.Length; i++)
			sb.Append(hashBytes[i].ToString("x2"));

		return sb.ToString();
	}
}
