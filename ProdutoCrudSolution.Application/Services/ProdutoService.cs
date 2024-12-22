using FluentValidation;
using ProdutoCrudSolution.Domain.Entities;
using ProdutoCrudSolution.Domain.Interfaces;

namespace ProdutoCrudSolution.Application.Services
{
    public class ProdutoService
    {
        private readonly IRepositoryBase<Produto> _produtoRepository;
        private readonly IValidator<Produto> _validator;

        public ProdutoService(IRepositoryBase<Produto> produtoRepository, IValidator<Produto> validator)
        {
            _produtoRepository = produtoRepository;
            _validator = validator;
        }

        public async Task<Produto> CreateProdutoAsync(Produto produto)
        {
            produto.DataCadastro = DateTime.Now;

            await _validator.ValidateAndThrowAsync(produto);

            await _produtoRepository.AddAsync(produto);
            await _produtoRepository.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> UpdateProdutoAsync(int id, Produto produtoAtualizado)
        {
            var produtoExistente = await _produtoRepository.GetByIdAsync(id);
            if (produtoExistente == null)
                throw new Exception("Produto não encontrado.");

            produtoExistente.Nome = produtoAtualizado.Nome;
            produtoExistente.Descricao = produtoAtualizado.Descricao;
            produtoExistente.Preco = produtoAtualizado.Preco;

            await _validator.ValidateAndThrowAsync(produtoExistente);

            await _produtoRepository.UpdateAsync(produtoExistente);
            await _produtoRepository.SaveChangesAsync();
            return produtoExistente;
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _produtoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task DeleteProdutoAsync(int id)
        {
            var produtoExistente = await _produtoRepository.GetByIdAsync(id);
            if (produtoExistente == null)
                throw new Exception("Produto não encontrado.");

            await _produtoRepository.DeleteAsync(produtoExistente);
            await _produtoRepository.SaveChangesAsync();
        }
    }
}
