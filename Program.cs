using Equilibrium.Api.Data;
using Equilibrium.Api.Middleware;
using Equilibrium.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// Ponto de entrada da aplicação Equilibrium API.
/// Responsável por configurar serviços, middlewares e iniciar o servidor.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Obtém a configuração da aplicação (appsettings.json, variáveis ambiente, etc.)
/// </summary>
var configuration = builder.Configuration;


/// <summary>
/// Adiciona suporte necessário aos Controllers para fazê-los funcionar corretamente.
/// </summary>
builder.Services.AddControllers();


/// <summary>
/// Lê a connection string "DefaultConnection" do appsettings.json
/// </summary>
var conn = configuration.GetConnectionString("DefaultConnection");

/// <summary>
/// Configura o Entity Framework Core para usar MySQL.
/// </summary>
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(conn, ServerVersion.AutoDetect(conn)));


/// <summary>
/// Configura versionamento da API.
/// </summary>
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});


/// <summary>
/// Injeta a implementação de IUserService.
/// O ASP.NET criará uma nova instância por requisição.
/// </summary>
builder.Services.AddScoped<IUserService, Equilibrium.Api.Services.UserService>();


/// <summary>
/// Necessário para gerar documentação Swagger.
/// </summary>
builder.Services.AddEndpointsApiExplorer();

/// <summary>
/// Adiciona e configura o Swagger.
/// </summary>
builder.Services.AddSwaggerGen();


/// <summary>
/// Constrói o aplicativo Web após registrar todos os serviços.
/// </summary>
var app = builder.Build();


/// <summary>
/// Ativa o middleware global de tratamento de exceções customizado.
/// Todas as exceções passam por esse middleware.
/// </summary>
app.UseGlobalExceptionMiddleware();


/// <summary>
/// Ativa Swagger somente em ambiente de desenvolvimento.
/// Disponibiliza documentação interativa em /swagger
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Equilibrium API V1");
    });
}

/// <summary>
/// Mapeia automaticamente todos os Controllers (e seus endpoints).
/// </summary>
app.MapControllers();


/// <summary>
/// Inicia o servidor web e mantém a API rodando.
/// </summary>
app.Run();
