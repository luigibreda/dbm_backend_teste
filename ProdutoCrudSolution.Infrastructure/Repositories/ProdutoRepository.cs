using Microsoft.EntityFrameworkCore;
using ProdutoCrudSolution.Domain.Entities;
using ProdutoCrudSolution.Domain.Interfaces;
using ProdutoCrudSolution.Infrastructure.Context;

namespace ProdutoCrudSolution.Infrastructure.Repositories
{
    public class ProdutoRepository : IRepositoryBase<Produto>
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> AddAsync(Produto entity)
        {
            await _context.Produtos.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(Produto entity)
        {
            _context.Produtos.Remove(entity);
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task UpdateAsync(Produto entity)
        {
            _context.Produtos.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
