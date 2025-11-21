
using SizeFintech.Application.ViewModels.Cart.InsertInvoiceToCart.Response;

namespace SizeFintech.Application.UseCases.Cart.InsertInvoiceToCart
{
    public interface IInsertInvoiceToCartUseCase
    {
        Task<ResponseInsertInvoiceToCartViewModel> ExecuteAsync(int invoiceId);
    }
}
