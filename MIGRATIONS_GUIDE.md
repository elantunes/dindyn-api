# 🚀 Guia Completo de Migrations - Entity Framework Core

## 📋 Índice
1. [Instalação e Configuração](#instalação-e-configuração)
2. [Comandos Básicos](#comandos-básicos)
3. [Cenários Práticos](#cenários-práticos)
4. [Troubleshooting](#troubleshooting)
5. [Exemplos Avançados](#exemplos-avançados)

---

## 🛠️ Instalação e Configuração

### 1. Instalar Entity Framework Core Tools
```bash
# Instalar globalmente
dotnet tool install --global dotnet-ef

# Verificar instalação
dotnet ef --version
```

### 2. Verificar Dependências no Projeto
Seu projeto já tem as dependências necessárias no `Dindyn.Infra.csproj`:
- ✅ `Microsoft.EntityFrameworkCore.Tools`
- ✅ `Microsoft.EntityFrameworkCore`
- ✅ `Pomelo.EntityFrameworkCore.MySql`

### 3. Configuração Automática do Banco
O projeto está configurado para criar o banco automaticamente quando rodar:

```csharp
// Dindyn.Api/Program.cs
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DindynDbContext>();
    
    try
    {
        // Aplicar migrations automaticamente
        context.Database.Migrate();
        Console.WriteLine("✅ Banco de dados e migrations aplicadas com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao aplicar migrations: {ex.Message}");
        // Em caso de erro, tentar criar o banco sem migrations
        try
        {
            context.Database.EnsureCreated();
            Console.WriteLine("✅ Banco de dados criado com sucesso!");
        }
        catch (Exception ex2)
        {
            Console.WriteLine($"❌ Erro ao criar banco: {ex2.Message}");
        }
    }
}
```

---

## 📝 Comandos Básicos

### 🎯 **Comandos Essenciais**

```bash
# Navegar para o projeto de infraestrutura
cd Dindyn.Infra

# 1. Criar nova migration
dotnet ef migrations add NomeDaMigration --startup-project ../Dindyn.Api

# 2. Aplicar migrations ao banco
dotnet ef database update --startup-project ../Dindyn.Api

# 3. Listar todas as migrations
dotnet ef migrations list --startup-project ../Dindyn.Api

# 4. Remover última migration (se não aplicada)
dotnet ef migrations remove --startup-project ../Dindyn.Api

# 5. Gerar script SQL
dotnet ef migrations script --startup-project ../Dindyn.Api
```

### 🔍 **Comandos de Diagnóstico**

```bash
# Verificar status detalhado
dotnet ef database update --startup-project ../Dindyn.Api --verbose

# Gerar script de uma migration específica
dotnet ef migrations script NomeDaMigration --startup-project ../Dindyn.Api

# Verificar se banco está sincronizado
dotnet ef migrations list --startup-project ../Dindyn.Api
```

---

## 🔄 Cenários Práticos

### **Cenário 1: Primeira Migration (Banco Existente)**

Se você já tem tabelas no banco e quer começar a usar migrations:

```bash
# 1. Criar migration inicial
dotnet ef migrations add InitialCreate --startup-project ../Dindyn.Api

# 2. Se as tabelas já existem, marcar como aplicada manualmente
# Execute no MySQL:
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20250909041107_InitialCreate', '8.0.0');
```

### **Cenário 2: Adicionar Nova Coluna**

```csharp
// 1. Modificar entidade
public class Cliente
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Nome { get; set; } // ← Nova propriedade
    public DateTime DataCriacao { get; set; } // ← Nova propriedade
}

// 2. Configurar no DbContext
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
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

```bash
# 3. Criar migration
dotnet ef migrations add AddNomeAndDataCriacaoToCliente --startup-project ../Dindyn.Api

# 4. Aplicar ao banco
dotnet ef database update --startup-project ../Dindyn.Api
```

### **Cenário 3: Criar Nova Tabela**

```csharp
// 1. Criar nova entidade
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
}

// 2. Adicionar DbSet no contexto
public class DindynDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<TokenAcesso> Tokens { get; set; }
    public DbSet<Produto> Produtos { get; set; } // ← Nova tabela
}

// 3. Configurar no OnModelCreating
modelBuilder.Entity<Produto>(entity =>
{
    entity.ToTable("produto");
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Id).ValueGeneratedOnAdd();
    entity.Property(e => e.Nome).IsRequired().HasMaxLength(255);
    entity.Property(e => e.Descricao).HasMaxLength(1000);
    entity.Property(e => e.Preco).HasColumnType("decimal(10,2)");
    entity.Property(e => e.Ativo).HasDefaultValue(true);
    entity.Property(e => e.DataCriacao).HasDefaultValue(DateTime.Now);
});
```

```bash
# 4. Criar e aplicar migration
dotnet ef migrations add AddProdutoTable --startup-project ../Dindyn.Api
dotnet ef database update --startup-project ../Dindyn.Api
```

### **Cenário 4: Adicionar Relacionamento**

```csharp
// 1. Modificar entidades
public class Cliente
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public List<TokenAcesso> Tokens { get; set; } = new(); // ← Relacionamento
}

public class TokenAcesso
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } // ← Navegação
    public string Token { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataValidade { get; set; }
}

// 2. Configurar relacionamento
modelBuilder.Entity<TokenAcesso>(entity =>
{
    entity.ToTable("token_acesso");
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Id).ValueGeneratedOnAdd();
    entity.Property(e => e.ClienteId).IsRequired();
    entity.Property(e => e.Token).IsRequired().HasMaxLength(45);
    entity.Property(e => e.DataCriacao).HasDefaultValue(DateTime.Now);
    entity.Property(e => e.DataValidade).IsRequired();
    
    // Configurar relacionamento
    entity.HasOne(t => t.Cliente)
          .WithMany(c => c.Tokens)
          .HasForeignKey(t => t.ClienteId)
          .OnDelete(DeleteBehavior.Cascade);
});
```

### **Cenário 5: Adicionar Índices**

```csharp
// No OnModelCreating
modelBuilder.Entity<Cliente>(entity =>
{
    // Índice único no email
    entity.HasIndex(e => e.Email).IsUnique();
    
    // Índice composto
    entity.HasIndex(e => new { e.Email, e.DataCriacao });
});

modelBuilder.Entity<TokenAcesso>(entity =>
{
    // Índice no token para busca rápida
    entity.HasIndex(e => e.Token).IsUnique();
    
    // Índice no ClienteId para joins
    entity.HasIndex(e => e.ClienteId);
});
```

---

## 🚨 Troubleshooting

### **Erro: "Table already exists"**
```bash
# Problema: Tentando aplicar migration em banco que já tem as tabelas
# Solução: Marcar migration como aplicada manualmente

# 1. Conectar ao MySQL
mysql -u root -p

# 2. Selecionar banco
USE dindyn;

# 3. Inserir registro da migration
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('NomeDaMigration', '8.0.0');
```

### **Erro: "Migration not found"**
```bash
# Problema: Migration foi removida mas ainda está no banco
# Solução: Remover do banco

# No MySQL:
DELETE FROM __EFMigrationsHistory WHERE MigrationId = 'NomeDaMigration';
```

### **Erro: "Connection string not found"**
```bash
# Problema: EF não consegue encontrar a connection string
# Solução: Usar --connection-string

dotnet ef database update --startup-project ../Dindyn.Api \
  --connection-string "Server=localhost;Database=dindyn;User Id=root;Password=#pantera9;"
```

### **Erro: "Build failed"**
```bash
# Problema: Erro de compilação
# Solução: Limpar e reconstruir

dotnet clean
dotnet build
dotnet ef migrations add NomeMigration --startup-project ../Dindyn.Api
```

---

## 🎯 Exemplos Avançados

### **Exemplo 1: Migration com Dados Iniciais**

```csharp
public partial class SeedInitialData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Inserir dados iniciais
        migrationBuilder.InsertData(
            table: "cliente", // ← Nome correto da tabela
            columns: new[] { "Email", "Senha", "Nome" },
            values: new object[,]
            {
                { "admin@dindyn.com", "admin123", "Administrador" },
                { "user@dindyn.com", "user123", "Usuário Teste" }
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
    }
}
```

### **Exemplo 2: Migration com Alteração de Coluna**

```csharp
public partial class AlterSenhaColumn : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Alterar tipo da coluna
        migrationBuilder.AlterColumn<string>(
            name: "Senha",
            table: "cliente", // ← Nome correto da tabela
            type: "varchar(255)",
            maxLength: 255,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(40)",
            oldMaxLength: 40);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Reverter alteração
        migrationBuilder.AlterColumn<string>(
            name: "Senha",
            table: "cliente", // ← Nome correto da tabela
            type: "varchar(40)",
            maxLength: 40,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(255)",
            oldMaxLength: 255);
    }
}
```

### **Exemplo 3: Migration com Renomeação**

```csharp
public partial class RenameClienteTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Renomear tabela (se necessário)
        migrationBuilder.RenameTable(
            name: "usuario",
            newName: "cliente");
            
        // Renomear coluna
        migrationBuilder.RenameColumn(
            name: "Senha",
            table: "cliente",
            newName: "PasswordHash");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Reverter renomeações
        migrationBuilder.RenameColumn(
            name: "PasswordHash",
            table: "cliente",
            newName: "Senha");
            
        migrationBuilder.RenameTable(
            name: "cliente",
            newName: "usuario");
    }
}
```

---

## 🔧 Dicas Importantes

### ✅ **Boas Práticas**
1. **Sempre faça backup** antes de aplicar migrations em produção
2. **Teste migrations** em ambiente de desenvolvimento primeiro
3. **Use nomes descritivos** para as migrations
4. **Revise o código gerado** antes de aplicar
5. **Mantenha migrations pequenas** e focadas
6. **Nunca edite migrations** que já foram aplicadas em produção

### ⚠️ **Cuidados Especiais**
1. **Migrations são irreversíveis** em produção (exceto com Down())
2. **Sempre teste o Down()** das migrations
3. **Mantenha histórico** das migrations aplicadas
4. **Use transações** para migrations críticas

---

## 📁 Estrutura de Arquivos

```
Dindyn.Infra/
├── Migrations/
│   ├── 20250909041107_InitialCreate.cs
│   ├── 20250909041107_InitialCreate.Designer.cs
│   └── DindynDbContextModelSnapshot.cs
├── Data/
│   └── Contexts/
│       └── DindynDbContext.cs
└── Scripts/
    └── migrations.sql
```

---

## 🚀 Comandos Rápidos

```bash
# Criar migration
dotnet ef migrations add NomeMigration --startup-project ../Dindyn.Api

# Aplicar migration
dotnet ef database update --startup-project ../Dindyn.Api

# Remover migration
dotnet ef migrations remove --startup-project ../Dindyn.Api

# Listar migrations
dotnet ef migrations list --startup-project ../Dindyn.Api

# Gerar script SQL
dotnet ef migrations script --startup-project ../Dindyn.Api

# Verificar status
dotnet ef database update --startup-project ../Dindyn.Api --verbose
```

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

**🎉 Agora você está pronto para usar migrations como um profissional!**
