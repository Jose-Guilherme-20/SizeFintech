
using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmpresaEntity> Empresa { get; set; }
        public DbSet<NotaFiscalEntity> NotaFiscal { get; set; }
        public DbSet<CarrinhoEntity> Carrinho { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
