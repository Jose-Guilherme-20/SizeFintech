
using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Models.Entities;
using SizeFintech.Infra.Context.Interceptor;

namespace SizeFintech.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options, AuditInterceptor auditInterceptor) : base(options)
        {
        }

        public DbSet<CompanyEntity> Empresa { get; set; }
        public DbSet<InvoiceEntity> NotaFiscal { get; set; }
        public DbSet<CartEntity> Carrinho { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
