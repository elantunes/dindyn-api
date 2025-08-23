using System.Text.RegularExpressions;

namespace Dindyn.Api.Routing;

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
	public string? TransformOutbound(object? value)
	{
		// Transforma em lowercase
		return value == null ? null : Regex.Replace(
			value.ToString()!,
			"([a-z])([A-Z])",
			"$1-$2",
			RegexOptions.CultureInvariant,
			TimeSpan.FromMilliseconds(100)
		).ToLowerInvariant();
	}
}
