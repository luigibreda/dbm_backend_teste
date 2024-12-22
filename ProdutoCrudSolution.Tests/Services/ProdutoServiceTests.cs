using Xunit;
using Moq;
using FluentValidation;
using ProdutoCrudSolution.Application.Services;
using ProdutoCrudSolution.Domain.Entities;
using ProdutoCrudSolution.Domain.Interfaces;
using FluentValidation.Results;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ProdutoCrudSolution.Tests.Services
{
    public class ProdutoServiceTests
    {
        private readonly ProdutoService _produtoService;
        private readonly Mock<IRepositoryBase<Produto>> _produtoRepositoryMock;
        private readonly Mock<IValidator<Produto>> _validatorMock;

        public ProdutoServiceTests()
        {
            _produtoRepositoryMock = new Mock<IRepositoryBase<Produto>>();
            _validatorMock = new Mock<IValidator<Produto>>();

            _validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<Produto>(), default))
                .ReturnsAsync(new ValidationResult());

            _produtoService = new ProdutoService(
                _produtoRepositoryMock.Object,
                _validatorMock.Object
            );
        }

        [Fact]
        public async Task Deve_Obter_Produto_Pelo_Id()
        {
            var produto = new Produto { Id = 1, Nome = "Produto Teste", Preco = 10.00m, Descricao = "Descrição válida." };
            _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produto);

            var resultado = await _produtoService.GetProdutoByIdAsync(1);

            Assert.Equal("Produto Teste", resultado.Nome);
            _produtoRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Deletar_Produto()
        {
            var produtoId = 2;
            var produto = new Produto { Id = produtoId, Nome = "Produto para Deletar", Preco = 20.00m, Descricao = "Descrição do produto a ser deletado." };

            _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(produtoId)).ReturnsAsync(produto);
            _produtoRepositoryMock.Setup(repo => repo.DeleteAsync(produto)).Returns(Task.CompletedTask);
            _produtoRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            await _produtoService.DeleteProdutoAsync(produtoId);

            _produtoRepositoryMock.Verify(repo => repo.GetByIdAsync(produtoId), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.DeleteAsync(produto), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Adicionar_Produto()
        {
            var produto = new Produto { Id = 3, Nome = "Produto Novo", Preco = 30.00m, Descricao = "Nova descrição." };

            _produtoRepositoryMock.Setup(repo => repo.AddAsync(produto)).ReturnsAsync(produto);
            _produtoRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            await _produtoService.CreateProdutoAsync(produto);

            _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<ValidationContext<Produto>>(), It.IsAny<CancellationToken>()), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Produto>(p => p == produto)), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }


        //[Fact]
        //public async Task Deve_Atualizar_Produto()
        //{
        //    var produtoId = 1;
        //    var produtoAtualizado = new Produto { Id = produtoId, Nome = "Produto Atualizado", Preco = 15.00m, Descricao = "Descrição atualizada." };
        //    var produtoExistente = new Produto { Id = produtoId, Nome = "Produto Original", Preco = 10.00m, Descricao = "Descrição original." };

        //    _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(produtoId)).ReturnsAsync(produtoExistente);
        //    _produtoRepositoryMock.Setup(repo => repo.UpdateAsync(produtoExistente)).Returns(Task.CompletedTask);
        //    _produtoRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

        //    await _produtoService.CreateProdutoAsync(produtoAtualizado);

        //    _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<ValidationContext<Produto>>(), It.IsAny<CancellationToken>()), Times.Once);
        //    _produtoRepositoryMock.Verify(repo => repo.GetByIdAsync(produtoId), Times.Once);
        //    _produtoRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Produto>(p => p == produtoExistente)), Times.Once);
        //    _produtoRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        //}



        [Fact]
        public async Task Deve_Criar_Produto_Quando_Dados_Sao_Validos()
        {
            var produto = new Produto { Id = 4, Nome = "Produto Teste", Preco = 10.00m, Descricao = "Descrição válida." };

            _produtoRepositoryMock.Setup(repo => repo.AddAsync(produto)).ReturnsAsync(produto);
            _produtoRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            var resultado = await _produtoService.CreateProdutoAsync(produto); // Use o nome correto

            Assert.NotNull(resultado);
            Assert.Equal("Produto Teste", resultado.Nome);
            _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<ValidationContext<Produto>>(), It.IsAny<CancellationToken>()), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Produto>(p => p == produto)), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        //[Fact]
        //public async Task Deve_Lancar_Excecao_Quando_Validacao_Falha()
        //{
        //    var produto = new Produto { Nome = "Inválido", Preco = 10.00m, Descricao = "Descrição inválida." };

        //    var failures = new List<ValidationFailure>
        //    {
        //        new ValidationFailure("Nome", "Nome é obrigatório."),
        //        new ValidationFailure("Descricao", "Descrição é inválida.")
        //    };

        //    _validatorMock
        //        .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<Produto>>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(new ValidationResult(failures));

        //    var exception = await Assert.ThrowsAsync<ValidationException>(() => _produtoService.CreateProdutoAsync(produto));
        //    Assert.Contains("Nome é obrigatório.", exception.Message);
        //    Assert.Contains("Descrição é inválida.", exception.Message);

        //    _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<ValidationContext<Produto>>(), It.IsAny<CancellationToken>()), Times.Once);
        //    _produtoRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Produto>()), Times.Never);
        //    _produtoRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        //}

    }
}
