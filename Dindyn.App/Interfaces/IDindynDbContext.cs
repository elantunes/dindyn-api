using Dindyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dindyn.App.Interfaces;

public interface IDindynDbContext
{
	DbSet<Domain.Entities.Cliente> Clientes { get; set; }
	DbSet<TokenAcesso> TokensAcesso { get; set; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}