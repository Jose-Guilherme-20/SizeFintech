
using SizeFintech.Infra.Context;

namespace SizeFintech.Infra.Repository
{
    public class InvoiceRepository : EntityBaseRepository<Domain.Models.Entities.InvoiceEntity>, Domain.Interfaces.Repository.IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
