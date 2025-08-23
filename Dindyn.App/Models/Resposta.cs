namespace Dindyn.App.Models;

public class Resposta
{
	public bool IsValid { get; set; }
	public object? Result { get; set; }
	public List<string> Errors { get; set; } = [];
}
