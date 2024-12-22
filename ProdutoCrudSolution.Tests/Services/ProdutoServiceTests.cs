using Xunit;
using Moq;
using FluentValidation;
using ProdutoCrudSolution.Application.Services;
using ProdutoCrudSolution.Domain.Entities;
using ProdutoCrudSolution.Domain.Interfaces;
using FluentValidation.Results;

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

            // Por padrão, a validação passa (sem erros)
            _validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<Produto>(), default))
                .ReturnsAsync(new ValidationResult());

            _produtoService = new ProdutoService(
                _produtoRepositoryMock.Object, 
                _validatorMock.Object
            );
        }

        [Fact]
        public async Task CreateProdutoAsync_DeveCriarProduto_QuandoDadosValidos()
        {
            // Arrange
            var produto = new Produto { Nome = "Produto Teste", Preco = 10 };

            // Act
            var result = await _produtoService.CreateProdutoAsync(produto);

            // Assert
            Assert.NotNull(result);
            _produtoRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Produto>()), Times.Once);
            _produtoRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateProdutoAsync_DeveLancarExcecao_QuandoValidacaoFalha()
        {
            // Arrange
            var produto = new Produto { Nome = "Invalido", Preco = 10 };

            // Forçando erro de validação
            _validatorMock
                .Setup(v => v.ValidateAsync(produto, default))
                .ThrowsAsync(new ValidationException("Erro de Validação"));

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _produtoService.CreateProdutoAsync(produto));
        }
    }
}
