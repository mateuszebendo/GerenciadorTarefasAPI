using Application.Contracts;
using Application.Profiles;
using Application.Services;
using Domain.Contracts;
using Domain.Services;
using Infrastructure.Config;
using Infrastructure.Repositories;
using Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

DataBaseConfig.Initialize(configuration);

builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ITarefaService, TarefaService>();
builder.Services.AddScoped<ITarefaDomainService, TarefaDomainService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddAutoMapper(typeof(TarefaProfile));
builder.Services.AddAutoMapper(typeof(UsuarioProfile));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilters>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
