namespace Dindyn.Api.Models;

public class Resposta<T>
{
	public bool IsValid { get; set; }
	public T? Result { get; set; }
	public List<string> Errors { get; set; } = [];
}
