using Microsoft.AspNetCore.Mvc;
using ProdutoCrudSolution.Mvc.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProdutoCrudSolution.Mvc.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ProdutosController(IConfiguration configuration)
        {
            _apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl");
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            var url = $"{_apiBaseUrl}/produtos"; // ex: http://localhost:8080/api/produtos
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var produtos = JsonSerializer.Deserialize<List<ProdutoViewModel>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(produtos);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var url = $"{_apiBaseUrl}/produtos";
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Erro ao criar produto");
            return View(model);
        }
    }
}
