# 🎯 Exemplos Práticos de Migrations - Projeto Dindyn

## 📋 Cenários Reais do Projeto Dindyn

### **Exemplo 1: Adicionar Campo Nome ao Cliente**

#### 1. Modificar a entidade Cliente
```csharp
// Dindyn.Domain/Entities/Cliente.cs
public class Cliente
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Nome { get; set; } // ← Novo campo
    public DateTime DataCriacao { get; set; } // ← Novo campo
}
```

#### 2. Configurar no DbContext
```csharp
// Dindyn.Infra/Data/Contexts/DindynDbContext.cs
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Cliente>(entity =>
    {
        entity.ToTable("cliente"); // ← Nome correto da tabela
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
        entity.Property(e => e.Senha).HasMaxLength(40);
        
        // Novas configurações
        entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
        entity.Property(e => e.DataCriacao)
              .IsRequired()
              .HasDefaultValue(DateTime.Now);
    });
}
```

#### 3. Criar e aplicar migration
```bash
cd Dindyn.Infra
dotnet ef migrations add AddNomeAndDataCriacaoToCliente --startup-project ../Dindyn.Api
dotnet ef database update --startup-project ../Dindyn.Api
```

---

### **Exemplo 2: Criar Tabela de Produtos**

#### 1. Criar entidade Produto
```csharp
// Dindyn.Domain/Entities/Produto.cs
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
}
```

#### 2. Adicionar DbSet no contexto
```csharp
// Dindyn.Infra/Data/Contexts/DindynDbContext.cs
public class DindynDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<TokenAcesso> Tokens { get; set; }
    public DbSet<Produto> Produtos { get; set; } // ← Nova tabela
}
```

#### 3. Configurar no OnModelCreating
```csharp
modelBuilder.Entity<Produto>(entity =>
{
    entity.ToTable("produto");
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Id).ValueGeneratedOnAdd();
    entity.Property(e => e.Nome).IsRequired().HasMaxLength(255);
    entity.Property(e => e.Descricao).HasMaxLength(1000);
    entity.Property(e => e.Preco).HasColumnType("decimal(10,2)");
    entity.Property(e => e.Estoque).HasDefaultValue(0);
    entity.Property(e => e.Ativo).HasDefaultValue(true);
    entity.Property(e => e.DataCriacao).HasDefaultValue(DateTime.Now);
    entity.Property(e => e.DataAtualizacao);
    
    // Índices
    entity.HasIndex(e => e.Nome);
    entity.HasIndex(e => e.Ativo);
});
```

#### 4. Criar e aplicar migration
```bash
cd Dindyn.Infra
dotnet ef migrations add AddProdutoTable --startup-project ../Dindyn.Api
dotnet ef database update --startup-project ../Dindyn.Api
```

---

### **Exemplo 3: Adicionar Relacionamento Cliente-Produto (Pedidos)**

#### 1. Criar entidade Pedido
```csharp
// Dindyn.Domain/Entities/Pedido.cs
public class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; }
    public DateTime DataPedido { get; set; }
    public List<ItemPedido> Itens { get; set; } = new();
}

// Dindyn.Domain/Entities/ItemPedido.cs
public class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
```

#### 2. Adicionar DbSets
```csharp
public DbSet<Pedido> Pedidos { get; set; }
public DbSet<ItemPedido> ItensPedido { get; set; }
```

#### 3. Configurar relacionamentos
```csharp
// Configuração do Pedido
modelBuilder.Entity<Pedido>(entity =>
{
    entity.ToTable("pedido");
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Id).ValueGeneratedOnAdd();
    entity.Property(e => e.ClienteId).IsRequired();
    entity.Property(e => e.ValorTotal).HasColumnType("decimal(10,2)");
    entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
    entity.Property(e => e.DataPedido).HasDefaultValue(DateTime.Now);
    
    // Relacionamento com Cliente
    entity.HasOne(p => p.Cliente)
          .WithMany()
          .HasForeignKey(p => p.ClienteId)
          .OnDelete(DeleteBehavior.Restrict);
    
    // Relacionamento com Itens
    entity.HasMany(p => p.Itens)
          .WithOne(i => i.Pedido)
          .HasForeignKey(i => i.PedidoId)
          .OnDelete(DeleteBehavior.Cascade);
});

// Configuração do ItemPedido
modelBuilder.Entity<ItemPedido>(entity =>
{
    entity.ToTable("item_pedido");
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Id).ValueGeneratedOnAdd();
    entity.Property(e => e.PedidoId).IsRequired();
    entity.Property(e => e.ProdutoId).IsRequired();
    entity.Property(e => e.Quantidade).IsRequired();
    entity.Property(e => e.PrecoUnitario).HasColumnType("decimal(10,2)");
    entity.Property(e => e.Subtotal).HasColumnType("decimal(10,2)");
    
    // Relacionamento com Produto
    entity.HasOne(i => i.Produto)
          .WithMany()
          .HasForeignKey(i => i.ProdutoId)
          .OnDelete(DeleteBehavior.Restrict);
});
```

#### 4. Criar migration
```bash
cd Dindyn.Infra
dotnet ef migrations add AddPedidoAndItemPedidoTables --startup-project ../Dindyn.Api
dotnet ef database update --startup-project ../Dindyn.Api
```

---

### **Exemplo 4: Adicionar Índices para Performance**

#### 1. Adicionar índices no DbContext
```csharp
// Índices para Cliente
modelBuilder.Entity<Cliente>(entity =>
{
    // Índice único no email
    entity.HasIndex(e => e.Email).IsUnique();
    
    // Índice composto para consultas
    entity.HasIndex(e => new { e.Email, e.DataCriacao });
});

// Índices para TokenAcesso
modelBuilder.Entity<TokenAcesso>(entity =>
{
    // Índice único no token
    entity.HasIndex(e => e.Token).IsUnique();
    
    // Índice no ClienteId para joins
    entity.HasIndex(e => e.ClienteId);
    
    // Índice composto para consultas de validação
    entity.HasIndex(e => new { e.ClienteId, e.DataValidade });
});

// Índices para Produto
modelBuilder.Entity<Produto>(entity =>
{
    // Índice no nome para busca
    entity.HasIndex(e => e.Nome);
    
    // Índice no status ativo
    entity.HasIndex(e => e.Ativo);
    
    // Índice composto para consultas
    entity.HasIndex(e => new { e.Ativo, e.Preco });
});

// Índices para Pedido
modelBuilder.Entity<Pedido>(entity =>
{
    // Índice no ClienteId para consultas
    entity.HasIndex(e => e.ClienteId);
    
    // Índice no status
    entity.HasIndex(e => e.Status);
    
    // Índice composto para relatórios
    entity.HasIndex(e => new { e.ClienteId, e.DataPedido });
});
```

#### 2. Criar migration
```bash
cd Dindyn.Infra
dotnet ef migrations add AddIndexesForPerformance --startup-project ../Dindyn.Api
dotnet ef database update --startup-project ../Dindyn.Api
```

---

### **Exemplo 5: Alterar Tipo de Coluna**

#### 1. Modificar propriedade
```csharp
// Alterar Senha de varchar(40) para varchar(255)
public class Cliente
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; } // Agora será varchar(255)
}
```

#### 2. Atualizar configuração
```csharp
modelBuilder.Entity<Cliente>(entity =>
{
    entity.ToTable("cliente");
    entity.Property(e => e.Senha).HasMaxLength(255); // ← Alterado de 40 para 255
});
```

#### 3. Criar migration
```bash
cd Dindyn.Infra
dotnet ef migrations add AlterSenhaColumnLength --startup-project ../Dindyn.Api
dotnet ef database update --startup-project ../Dindyn.Api
```

---

### **Exemplo 6: Adicionar Dados Iniciais (Seed Data)**

#### 1. Criar migration com dados
```bash
cd Dindyn.Infra
dotnet ef migrations add SeedInitialData --startup-project ../Dindyn.Api
```

#### 2. Editar a migration gerada
```csharp
// Dindyn.Infra/Migrations/XXXXXX_SeedInitialData.cs
protected override void Up(MigrationBuilder migrationBuilder)
{
    // Inserir clientes iniciais
    migrationBuilder.InsertData(
        table: "cliente", // ← Nome correto da tabela
        columns: new[] { "Email", "Senha", "Nome", "DataCriacao" },
        values: new object[,]
        {
            { "admin@dindyn.com", "admin123", "Administrador", DateTime.Now },
            { "user@dindyn.com", "user123", "Usuário Teste", DateTime.Now }
        });

    // Inserir produtos iniciais
    migrationBuilder.InsertData(
        table: "produto",
        columns: new[] { "Nome", "Descricao", "Preco", "Estoque", "Ativo", "DataCriacao" },
        values: new object[,]
        {
            { "Produto 1", "Descrição do produto 1", 29.99m, 100, true, DateTime.Now },
            { "Produto 2", "Descrição do produto 2", 49.99m, 50, true, DateTime.Now }
        });
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    // Remover dados inseridos
    migrationBuilder.DeleteData(
        table: "cliente", // ← Nome correto da tabela
        keyColumn: "Email",
        keyValue: "admin@dindyn.com");
        
    migrationBuilder.DeleteData(
        table: "cliente", // ← Nome correto da tabela
        keyColumn: "Email",
        keyValue: "user@dindyn.com");
        
    migrationBuilder.DeleteData(
        table: "produto",
        keyColumn: "Nome",
        keyValue: "Produto 1");
        
    migrationBuilder.DeleteData(
        table: "produto",
        keyColumn: "Nome",
        keyValue: "Produto 2");
}
```

#### 3. Aplicar migration
```bash
dotnet ef database update --startup-project ../Dindyn.Api
```

---

### **Exemplo 7: Renomear Tabela (se necessário)**

#### 1. Criar migration de renomeação
```bash
cd Dindyn.Infra
dotnet ef migrations add RenameUsuarioToCliente --startup-project ../Dindyn.Api
```

#### 2. Editar a migration gerada
```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    // Renomear tabela de usuario para cliente
    migrationBuilder.RenameTable(
        name: "usuario",
        newName: "cliente");
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    // Reverter renomeação
    migrationBuilder.RenameTable(
        name: "cliente",
        newName: "usuario");
}
```

---

## 🚀 Comandos Úteis para Desenvolvimento

### **Verificar Status**
```bash
# Listar todas as migrations
dotnet ef migrations list --startup-project ../Dindyn.Api

# Verificar se banco está sincronizado
dotnet ef database update --startup-project ../Dindyn.Api --dry-run
```

### **Rollback**
```bash
# Voltar para migration anterior
dotnet ef database update NomeDaMigrationAnterior --startup-project ../Dindyn.Api

# Voltar para o início (remover todas as migrations)
dotnet ef database update 0 --startup-project ../Dindyn.Api
```

### **Scripts SQL**
```bash
# Gerar script de todas as migrations
dotnet ef migrations script --startup-project ../Dindyn.Api

# Gerar script de uma migration específica
dotnet ef migrations script NomeDaMigration --startup-project ../Dindyn.Api

# Gerar script de uma migration para outra
dotnet ef migrations script MigrationInicial MigrationFinal --startup-project ../Dindyn.Api
```

### **Limpeza**
```bash
# Remover última migration (se não aplicada)
dotnet ef migrations remove --startup-project ../Dindyn.Api

# Limpar e reconstruir
dotnet clean
dotnet build
```

---

## ⚠️ Cuidados Importantes

1. **Sempre teste** migrations em ambiente de desenvolvimento primeiro
2. **Faça backup** do banco antes de aplicar em produção
3. **Revise o código** gerado nas migrations
4. **Nunca edite** migrations que já foram aplicadas em produção
5. **Use transações** para migrations críticas
6. **Mantenha migrations pequenas** e focadas

---

## 🎯 Fluxo de Trabalho Recomendado

1. **Desenvolvimento:**
   - Modificar entidades
   - Configurar no DbContext
   - Criar migration
   - Testar localmente

2. **Teste:**
   - Aplicar migration em ambiente de teste
   - Validar funcionamento
   - Testar rollback se necessário

3. **Produção:**
   - Fazer backup do banco
   - Aplicar migration
   - Monitorar aplicação
   - Documentar mudanças

---

**🎉 Agora você tem exemplos práticos atualizados para usar migrations no seu projeto Dindyn!**
