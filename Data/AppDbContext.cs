using System;
using Microsoft.EntityFrameworkCore;
using Carrinho_Shopee.Models;

namespace Carrinho_Shopee.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

public DbSet<Carrinho_Shopee.Models.Produto> Produto { get; set; } = default!;
}
