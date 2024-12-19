using ProdutoAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProdutoAPI.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(int id);
        Task Add(Produto produto);
        Task Update(Produto produto);
        Task Delete(int id);
    }
}
