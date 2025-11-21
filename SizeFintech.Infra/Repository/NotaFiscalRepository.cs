
using SizeFintech.Infra.Context;

namespace SizeFintech.Infra.Repository
{
    public class NotaFiscalRepository : EntityBaseRepository<Domain.Models.Entities.NotaFiscalEntity>, Domain.Interfaces.Repository.INotaFiscalRepository
    {
        public NotaFiscalRepository(AppDbContext context) : base(context)
        {
        }
    }
}
