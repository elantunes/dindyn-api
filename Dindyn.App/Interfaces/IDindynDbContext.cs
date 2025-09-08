using Dindyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dindyn.App.Interfaces;

public interface IDindynDbContext
{
	DbSet<TokenAcesso> Tokens { get; set; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}