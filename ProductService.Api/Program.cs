using ProductService.Application;
using ProductService.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetConnectionString("DbConnection") ?? "emptyConnection";

// Add services to the container.
ApplicationDependencyInjection.RegisterMediatr(builder.Services);
InfrastructureDependencyInjection.RegisterDbContext(builder.Services, dbConnectionString);
InfrastructureDependencyInjection.RegisterInfrastructure(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
