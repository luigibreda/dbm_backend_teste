using FluentValidation;
using ProdutoCrudSolution.Domain.Entities;
using ProdutoCrudSolution.Domain.Interfaces;

namespace ProdutoCrudSolution.Application.Validations
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        private readonly IRepositoryBase<Produto> _produtoRepository;

        public ProdutoValidator(IRepositoryBase<Produto> produtoRepository)
        {
            _produtoRepository = produtoRepository;

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do produto deve ter no máximo 100 caracteres.")
                .MustAsync(async (nome, cancellation) =>
                {
                    var produtos = await _produtoRepository.GetAllAsync();
                    return !produtos.Any(p => p.Nome.ToLower() == nome.ToLower());
                }).WithMessage("Já existe um produto com esse nome.");

            RuleFor(x => x.Preco)
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");
        }
    }
}
