using Dindyn.Api;
using Dindyn.Infra;

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
		options.Filters.Add(typeof(ChaveSegurancaAttribute));
	});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Ioc.AddIDependencyInjection(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
