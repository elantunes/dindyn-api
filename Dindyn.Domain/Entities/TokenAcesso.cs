namespace Dindyn.Domain.Entities;

public class TokenAcesso
{
	public int Id { get; set; }
	public int ClienteId { get; set; }
	public required string Token { get; set; }
	public DateTime DataCriacao { get; set; }
}
