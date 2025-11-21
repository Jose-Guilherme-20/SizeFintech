
using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CompanyEntity> Empresa { get; set; }
        public DbSet<InvoiceEntity> NotaFiscal { get; set; }
        public DbSet<CartEntity> Carrinho { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is null)
                    continue;

                if (entry.State == EntityState.Added)
                {
                    SetDefaultDate(entry, "DataCriacao", DateTime.UtcNow);
                }

                if (entry.State == EntityState.Deleted)
                {
                    if (HasProperty(entry, "DataExclusao") && HasProperty(entry, "Ativo"))
                    {
                        entry.State = EntityState.Modified;
                        entry.Property("DataExclusao").CurrentValue = DateTime.UtcNow;
                        entry.Property("Ativo").CurrentValue = false;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }


        private static bool HasProperty(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry, string propName)
        {
            return entry.Properties.Any(p => p.Metadata.Name == propName);
        }

        private static void SetDefaultDate(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry, string propName, DateTime date)
        {
            if (HasProperty(entry, propName))
            {
                var current = entry.Property(propName).CurrentValue;

                if (current is null || (DateTime)current < new DateTime(1753, 1, 1))
                {
                    entry.Property(propName).CurrentValue = date;
                }
            }
        }
    }
}
