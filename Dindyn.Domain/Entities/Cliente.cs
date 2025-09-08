namespace Dindyn.Domain.Entities;

public class Cliente
{
	public int Id { get; set; }
	public required string Email { get; set; }
	public required string Senha { get; set; }
}
