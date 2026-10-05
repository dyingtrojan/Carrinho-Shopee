using Carrinho_Shopee.Data;
using Carrinho_Shopee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Carrinho_Shopee.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly AppDbContext _context;
        // GET: CarrinhoController

        public CarrinhoController(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public ActionResult Index()
        {
            var carrinho = ObterCarrinho();
            return View(carrinho);
        }
        public async Task<IActionResult> Adicionar(int id)
        {
            var produto = await _context.Produto.FirstOrDefaultAsync(p => p.Codigo == id);
            if (produto == null)
            {
                return NotFound();
            }
            var carrinho = ObterCarrinho();

            var item = carrinho.FirstOrDefault(
                x => x.ProdutoId == produto.Codigo
            );

            if (item == null)
            {
                carrinho.Add(new CarrinhoItem
                {
                    ProdutoId = produto.Codigo,
                    Nome = produto.Nome,
                    Preco = produto.Preco,
                    Quantidade = 1
                });
            } else
            {
                item.Quantidade++;
            }
            SalvarCarrinho(carrinho);
            return RedirectToAction("Index");
        }

        private List<CarrinhoItem> ObterCarrinho()
        {
            var carrinhoJson = HttpContext.Session.GetString("Carrinho");
            if (string.IsNullOrEmpty(carrinhoJson))
            {
                return new List<CarrinhoItem>();
            }
                return JsonSerializer.Deserialize<List<CarrinhoItem>>(carrinhoJson) ?? new List<CarrinhoItem>();
        }
        private void SalvarCarrinho(List<CarrinhoItem> carrinho)
        {
            var carrinhoJson = JsonSerializer.Serialize(carrinho);

            HttpContext.Session.SetString("Carrinho", carrinhoJson);
        }
        public IActionResult Remover(int id){
            var carrinho = ObterCarrinho();
            var item = carrinho.FirstOrDefault(x => x.ProdutoId == id);
            if (item != null){
                carrinho.Remove(item);
            }
            SalvarCarrinho(carrinho);
            return RedirectToAction(nameof(Index));
        }
    }
}
