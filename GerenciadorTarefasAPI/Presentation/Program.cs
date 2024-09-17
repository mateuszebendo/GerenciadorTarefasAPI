using Application.Contracts;
using Application.Profiles;
using Application.Services;
using Domain.Contracts;
using Infrastructure.Config;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

DataBaseConfig.Initialize(configuration);

builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

builder.Services.AddScoped<ITarefaService, TarefaService>();

builder.Services.AddAutoMapper(typeof(TarefaProfile));

builder.Services.AddControllers();

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
