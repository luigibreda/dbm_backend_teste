using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProdutoCrudSolution.Infrastructure.Context;
using ProdutoCrudSolution.Domain.Entities;
using ProdutoCrudSolution.Domain.Interfaces;
using ProdutoCrudSolution.Infrastructure.Repositories;
using FluentValidation;
using ProdutoCrudSolution.Application.Validations;
using ProdutoCrudSolution.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura EntityFramework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("FakeDB"));


// Adiciona refer�ncias do seu CRUD
builder.Services.AddScoped<IRepositoryBase<Produto>, ProdutoRepository>();
builder.Services.AddScoped<IValidator<Produto>, ProdutoValidator>();
builder.Services.AddScoped<ProdutoService>();

// Controllers
builder.Services.AddControllers();

// ** Swagger **
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Opcionalmente, pode customizar o Doc:
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProdutoCrudSolution.Api", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Rota padr�o: Controllers
app.MapControllers();

// Inicia a aplica��o
app.Run();
