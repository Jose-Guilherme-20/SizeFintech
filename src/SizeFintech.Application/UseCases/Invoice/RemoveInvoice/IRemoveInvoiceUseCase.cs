
namespace SizeFintech.Application.UseCases.Invoice.RemoveInvoice
{
    public interface IRemoveInvoiceUseCase
    {
        Task ExecuteAsync(int invoiceId);
    }
}
