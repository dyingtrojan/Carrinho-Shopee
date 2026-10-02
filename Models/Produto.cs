using System.ComponentModel.DataAnnotations;

namespace Carrinho_Shopee.Models
{
    public class Produto
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public Decimal Preco { get; set; }
        public int Estoque { get; set; }
    }
}