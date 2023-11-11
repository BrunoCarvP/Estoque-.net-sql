using Estoque.Models;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Data
{
    public class BancoContext :DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }
        public DbSet<ItensEstoque> Itens  { get; set; }
    }
}
