using user.backend.application.Users;
using user.backend.domain.Common.Interfaces;
using user.backend.domain.Users.Interfaces;
using user.backend.infraestructure.Common;
using user.backend.infraestructure.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddScoped<ISqlServerConnection, SqlServerConnection>();
builder.Services.AddScoped<UserApp>();
builder.Services.AddScoped<IUserRepositoryQuery, UserRepositoryQuery>();
builder.Services.AddScoped<IUserRepositoryCommand, UserRepositoryCommand>();
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
