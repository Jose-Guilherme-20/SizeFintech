
using System.Threading.Tasks;
using SizeFintech.Application.ViewModels.Cart.RemoveInvoiceToCart.Response;

namespace SizeFintech.Application.UseCases.Cart.RemoveInvoiceToCart
{
    public interface IRemoveInvoiceToCartUSeCase
    {
        Task<ResponseRemoveInvoiceToCartViewModel> ExecuteAsync(int invoiceId);
    }
}
