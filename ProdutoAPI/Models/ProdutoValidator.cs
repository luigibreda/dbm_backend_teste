using FluentValidation;
using ProdutoAPI.Interfaces;
using ProdutoAPI.Models;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator(IProdutoRepository produtoRepository)
    {
        // Validação do nome
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres.")
            .Must((produto, nome) => !produtoRepository.ExisteProdutoComNome(nome, produto.Id))
            .WithMessage("Já existe um produto com esse nome.");

        // Validação do preço
        RuleFor(p => p.Preco)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        // Validação da descrição
        RuleFor(p => p.Descricao)
            .MaximumLength(250).WithMessage("A descrição não pode exceder 250 caracteres.");
    }
}
