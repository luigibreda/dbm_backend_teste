using FluentValidation;
using ProdutoAPI.Interfaces;
using ProdutoAPI.Models;
using ProdutoAPI.Repositories; // Exemplo de namespace para seu repositório

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator(IProdutoRepository produtoRepository)
    {
        // Validação do nome
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres.")
            .Must(nome => !produtoRepository.ExisteProdutoComNome(nome))
            .WithMessage("Já existe um produto com esse nome.");

        // Validação do preço
        RuleFor(p => p.Preco)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        // Validação da descrição
        RuleFor(p => p.Descricao)
            .MaximumLength(250).WithMessage("A descrição não pode exceder 250 caracteres.");
    }
}
