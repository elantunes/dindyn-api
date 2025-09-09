using Dindyn.App.Interfaces;
using Dindyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dindyn.Infra.Data.Contexts;

public class DindynDbContext(DbContextOptions<DindynDbContext> options) : DbContext(options), IDindynDbContext
{
	public DbSet<Cliente> Clientes { get; set; }
	public DbSet<TokenAcesso> TokensAcesso { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Aplicar convenções globais automaticamente
		ApplySnakeCaseConventions(modelBuilder);
		ApplyDateTimeConventions(modelBuilder);

		// Configurações específicas das entidades
		modelBuilder.Entity<Cliente>(entity =>
		{
			entity.ToTable("cliente");
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Id).ValueGeneratedOnAdd();
			entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
			entity.Property(e => e.Senha).HasMaxLength(64);
		});

		modelBuilder.Entity<TokenAcesso>(entity =>
		{
			entity.ToTable("token_acesso");
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Id).ValueGeneratedOnAdd();
			entity.Property(e => e.ClienteId).IsRequired();
			entity.Property(e => e.Token).IsRequired().HasMaxLength(64);
			entity.Property(e => e.DataCriacao)
				.HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
				.ValueGeneratedOnAdd();
			entity.Property(e => e.DataValidade).IsRequired();
		});
	}

	private static void ApplySnakeCaseConventions(ModelBuilder modelBuilder)
	{
		foreach (var entity in modelBuilder.Model.GetEntityTypes())
		{
			// Configurar nome da tabela em snake_case usando o nome da entidade (singular)
			var entityName = entity.ClrType.Name;
			entity.SetTableName(ToSnakeCase(entityName));

			// Configurar nomes das colunas em snake_case
			foreach (var property in entity.GetProperties())
				property.SetColumnName(ToSnakeCase(property.Name));
		}
	}

	private static void ApplyDateTimeConventions(ModelBuilder modelBuilder)
	{
		foreach (var entity in modelBuilder.Model.GetEntityTypes())
			// Configurar precisão de 3 para propriedades DateTime
			foreach (var property in entity.GetProperties())
				if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
					property.SetColumnType("datetime(3)");
	}

	private static string ToSnakeCase(string input)
	{
		if (string.IsNullOrEmpty(input))
			return input;

		// Converte PascalCase para snake_case
		return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
	}
}
