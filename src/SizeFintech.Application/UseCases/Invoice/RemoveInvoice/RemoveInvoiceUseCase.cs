
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;

namespace SizeFintech.Application.UseCases.Invoice.RemoveInvoice
{
    public class RemoveInvoiceUseCase
        (
          IInvoiceRepository invoiceRepository,
          IDomainNotification domainNotification,
          IUnitOfWork unitOfWork
        ) : IRemoveInvoiceUseCase
    {
        public async Task ExecuteAsync(int invoiceId)
        {
            var invoiceEntity = await invoiceRepository.GetByIdAsync(invoiceId);
            if (invoiceEntity is null)
            {
                domainNotification.AddNotification("Invoice", "Fatura não encontrada.");
                return;
            }
            invoiceRepository.Remove(invoiceEntity);
            await unitOfWork.CommitAsync();
        }
    }
}
