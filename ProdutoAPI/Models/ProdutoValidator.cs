using FluentValidation;
using ProdutoAPI.Models;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres.");

        RuleFor(p => p.Preco)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        RuleFor(p => p.Descricao)
            .MaximumLength(250).WithMessage("A descrição não pode exceder 250 caracteres.");
    }
}
