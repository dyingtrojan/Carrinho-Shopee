using System;

namespace Carrinho_Shopee.Models;

public class CarrinhoItem
{
    public int ProdutoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public decimal Subtotal
    {
        get{
            return Preco * Quantidade;
        }
    }
}
