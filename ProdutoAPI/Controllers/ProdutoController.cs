using Microsoft.AspNetCore.Mvc;
using ProdutoAPI.Interfaces;
using ProdutoAPI.Models;
using System.Threading.Tasks;

namespace ProdutoAPI.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.Add(produto);
                return RedirectToAction("Index"); 
            }

            return View(produto); 
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, Produto produto)
        //{
        //    if (id != produto.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        await _produtoRepository.Update(produto);
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(produto);
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _produtoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
