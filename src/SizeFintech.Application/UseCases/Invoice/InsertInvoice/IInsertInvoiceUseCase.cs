using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Request;
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Response;

namespace SizeFintech.Application.UseCases.Invoice.InsertInvoice
{
    public interface IInsertInvoiceUseCase
    {
        Task<ResponseInsertInvoiceViewModel> ExecuteAsync(RequestInsertInvoiceViewModel request);
    }
}
