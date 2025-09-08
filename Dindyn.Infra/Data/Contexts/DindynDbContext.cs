using Dindyn.App.Interfaces;
using Dindyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dindyn.Infra.Data.Contexts;

public class DindynDbContext(DbContextOptions<DindynDbContext> options) : DbContext(options), IDindynDbContext
{
	public DbSet<TokenAcesso> Tokens { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//Configuração da entidade Token
	 	modelBuilder.Entity<TokenAcesso>(entity =>
		{
			entity.ToTable("token_acesso");
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Id).ValueGeneratedOnAdd();
			entity.Property(e => e.ClienteId).IsRequired();
			entity.Property(e => e.Token).IsRequired().HasMaxLength(45);
			entity.Property(e => e.DataCriacao).HasDefaultValue(DateTime.Now);
		});
	}
}
