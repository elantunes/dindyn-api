using Dindyn.Api.Filters;
using Dindyn.Infra.Ioc;
using Dindyn.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
	options.Conventions.Add(new Microsoft.AspNetCore.Mvc.ApplicationModels.RouteTokenTransformerConvention(
		new Dindyn.Api.Routing.SlugifyParameterTransformer()));
});
builder.Services.AddControllers()
	.AddMvcOptions(options =>
	{
		options.Filters.Add<ApiExceptionFilter>();
		options.Filters.Add(typeof(ChaveSegurancaFilter));
	});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.OperationFilter<CustomHeaderParameterFilter>();
	c.SwaggerDoc("v1", new() { Title = "Dindyn API", Version = "v1" });
});

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Garantir que o banco de dados seja criado e as migrations aplicadas
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
