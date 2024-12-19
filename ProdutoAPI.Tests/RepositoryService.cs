using Xunit;
using Moq;
using ProdutoAPI.Repositories;
using ProdutoAPI.Models;
using ProdutoAPI.Interfaces;
using System.Threading.Tasks;
using FluentAssertions;

namespace Tests;

public class RepositoryService
{
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;

    public RepositoryService()
    {
        _produtoRepositoryMock = new Mock<IProdutoRepository>();
    }

    [Fact]
    public async Task Deve_Obter_Produto_Pelo_Id()
    {
        var produto = new Produto { Id = 1, Nome = "Produto Teste", Preco = 10.00m, Descricao = "Descrição válida." };
        _produtoRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(produto);

        var result = await _produtoRepositoryMock.Object.GetById(1);

        result.Should().NotBeNull();
        result.Nome.Should().Be("Produto Teste");
    }

    [Fact]
    public async Task Deve_Deletar_Produto()
    {
        var produtoId = 2;
        _produtoRepositoryMock
            .Setup(repo => repo.Delete(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        await _produtoRepositoryMock.Object.Delete(produtoId);

        _produtoRepositoryMock.Verify(repo => repo.Delete(It.Is<int>(id => id == produtoId)), Times.Once);
    }

    [Fact]
    public void Deve_Adicionar_Produto()
    {
        var produto = new Produto { Id = 2, Nome = "Produto Novo", Preco = 20.00m, Descricao = "Nova descrição." };

        _produtoRepositoryMock
            .Setup(repo => repo.Add(It.IsAny<Produto>()))
            .Returns(Task.CompletedTask);

        var result = _produtoRepositoryMock.Object.Add(produto);

        _produtoRepositoryMock.Verify(repo => repo.Add(It.Is<Produto>(p => p == produto)), Times.Once);
    }



    [Fact]
    public async Task Deve_Atualizar_Produto()
    {
        var produto = new Produto { Id = 1, Nome = "Produto Atualizado", Preco = 15.00m, Descricao = "Descrição atualizada." };
        _produtoRepositoryMock
            .Setup(repo => repo.Update(It.IsAny<Produto>()))
            .Returns(Task.CompletedTask);

        await _produtoRepositoryMock.Object.Update(produto);

        _produtoRepositoryMock.Verify(repo => repo.Update(It.Is<Produto>(p => p == produto)), Times.Once);
    }
}