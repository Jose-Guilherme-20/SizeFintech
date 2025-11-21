
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Request;

namespace SizeFintech.Application.Validations.Invoice.InsertInvoice
{
    public class InsertInvoiceUseCase : IInsertInvoiceUseCase
    {
        public async Task ExecuteAsync(RequestInsertInvoiceViewModel request)
        {
            await Task.CompletedTask;
        }
    }
}
