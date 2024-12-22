namespace ProdutoCrudSolution.Mvc.Models
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
