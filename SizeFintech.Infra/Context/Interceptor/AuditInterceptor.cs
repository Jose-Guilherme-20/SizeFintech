
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SizeFintech.Infra.Context.Interceptor
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var context = eventData.Context;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is null)
                    continue;

                // Criando novo registro
                if (entry.State == EntityState.Added)
                {
                    if (entry.Properties.Any(p => p.Metadata.Name == "DataCriacao"))
                    {
                        entry.Property("DataCriacao").CurrentValue = DateTime.UtcNow;
                    }
                }

                // Exclusão lógica
                if (entry.State == EntityState.Deleted)
                {
                    if (entry.Properties.Any(p => p.Metadata.Name == "DataExclusao"))
                    {
                        entry.State = EntityState.Modified; 
                        entry.Property("DataExclusao").CurrentValue = DateTime.UtcNow;
                        entry.Property("Ativo").CurrentValue = false;
                    }
                }
            }

            return base.SavingChanges(eventData, result);
        }
    }
}
