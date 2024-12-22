using Microsoft.AspNetCore.Mvc;
using ProdutoCrudSolution.Application.Services;
using ProdutoCrudSolution.Domain.Entities;

namespace ProdutoCrudSolution.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutosController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto == null)
                return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Produto produto)
        {
            try
            {
                var novoProduto = await _produtoService.CreateProdutoAsync(produto);
                return CreatedAtAction(nameof(GetById), new { id = novoProduto.Id }, novoProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Produto produto)
        {
            try
            {
                var produtoAtualizado = await _produtoService.UpdateProdutoAsync(id, produto);
                return Ok(produtoAtualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _produtoService.DeleteProdutoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
