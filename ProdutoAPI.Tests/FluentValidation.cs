using FluentValidation.TestHelper;
using ProdutoAPI.Models;
using Moq;
using ProdutoAPI.Interfaces;

namespace Tests;

public class FluentValidation
{
    private readonly ProdutoValidator _validador;
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;

    public FluentValidation()
    {
        _produtoRepositoryMock = new Mock<IProdutoRepository>();
        _validador = new ProdutoValidator(_produtoRepositoryMock.Object);
    }

    [Fact]
    public void Deve_Ter_Erro_Quando_Nome_Estiver_Vazio()
    {

        var produto = new Produto { Nome = "", Preco = 10.00m, Descricao = "Descrição válida." };

        var resultado = _validador.TestValidate(produto);

        resultado.ShouldHaveValidationErrorFor(p => p.Nome);
    }

    [Fact]
    public void Deve_Ter_Erro_Quando_Nome_Ja_Existir()
    {

        _produtoRepositoryMock
        .Setup(r => r.ExisteProdutoComNome(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(true);

        var produto = new Produto { Nome = "Produto Existente", Preco = 10.00m, Descricao = "Descrição válida." };

        var resultado = _validador.TestValidate(produto);

        resultado.ShouldHaveValidationErrorFor(p => p.Nome)
                 .WithErrorMessage("Já existe um produto com esse nome.");
    }

    [Fact]
    public void Nao_Deve_Ter_Erro_Quando_Nome_For_Valido()
    {

        _produtoRepositoryMock
        .Setup(r => r.ExisteProdutoComNome(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(false);

        var produto = new Produto { Nome = "Produto Novo", Preco = 10.00m, Descricao = "Descrição válida." };

        var resultado = _validador.TestValidate(produto);

        resultado.ShouldNotHaveValidationErrorFor(p => p.Nome);
    }

    [Fact]
    public void Deve_Ter_Erro_Quando_Preco_For_Menor_Que_Zero()
    {

        var produto = new Produto { Nome = "Produto", Preco = -1.00m, Descricao = "Descrição válida." };

        var resultado = _validador.TestValidate(produto);

        resultado.ShouldHaveValidationErrorFor(p => p.Preco);
    }
}
