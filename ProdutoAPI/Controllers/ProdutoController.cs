using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutoAPI.Interfaces;
using ProdutoAPI.Models;
using System.Threading.Tasks;

namespace ProdutoAPI.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IValidator<Produto> _produtoValidator;

        public ProdutoController(IProdutoRepository produtoRepository, IValidator<Produto> produtoValidator)
        {
            _produtoRepository = produtoRepository;
            _produtoValidator = produtoValidator;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoRepository.GetAll();
            return View(produtos);
        }

        //public async Task<IActionResult> Details(int id)
        //{
        //    var produto = await _produtoRepository.GetById(id);
        //    if (produto == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(produto);
        //}
        public IActionResult Create()
        {
            return View(new Produto());
        }

        [HttpPost]
        public IActionResult Create(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return View(produto); // Retorna a view com erros de validação
            }

            // Adicionar o produto ao banco de dados
            _produtoRepository.Add(produto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return View(produto);
            }

            var produtoExistente = await _produtoRepository.GetById(produto.Id);
            if (produtoExistente == null)
            {
                return NotFound();
            }

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Descricao = produto.Descricao;

            // Convertendo o preço para decimal antes de salvar
            produtoExistente.Preco = produto.Preco;

            await _produtoRepository.Update(produtoExistente);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }

            await _produtoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
