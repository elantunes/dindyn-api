using Dindyn.Api.Filters;
using Dindyn.Infra.Ioc;

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
