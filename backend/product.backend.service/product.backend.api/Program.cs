using Microsoft.AspNetCore.Hosting;
using product.backend.application.Products;
using product.backend.domain.Common.Interfaces;
using product.backend.domain.Products.Interfaces;
using product.backend.infraestructure.Common;
using product.backend.infraestructure.Products;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISqlServerConnection, SqlServerConnection>();
builder.Services.AddScoped<ProductApp>();
builder.Services.AddScoped<IProductRepositoryQuery, ProductRepositoryQuery>();
builder.Services.AddScoped<IProductRepositoryCommand, ProductRepositoryCommand>();
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
