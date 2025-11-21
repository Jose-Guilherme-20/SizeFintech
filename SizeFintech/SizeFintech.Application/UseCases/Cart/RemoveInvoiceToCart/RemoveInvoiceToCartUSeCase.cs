
using SizeFintech.Application.ViewModels.Cart.RemoveInvoiceToCart.Response;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;
using SizeFintech.Domain.Models.Entities;
using SizeFintech.Domain.Models.Notification;
using SizeFintech.Infra.Repository;
using SizeFintech.Infra.UnitOfWork;

namespace SizeFintech.Application.UseCases.Cart.RemoveInvoiceToCart
{
    public class RemoveInvoiceToCartUSeCase
        (
            IInvoiceRepository invoiceRepository,
            ICartRepository cartRepository,
            IUnitOfWork unitOfWork,
            IDomainNotification domainNotification
        )
            : IRemoveInvoiceToCartUSeCase
    {
        public async Task<ResponseRemoveInvoiceToCartViewModel> ExecuteAsync(int invoiceId)
        {
            var invoiceEntity = await invoiceRepository.GetByIdAsync(invoiceId);
            if (invoiceEntity is null)
            {
                domainNotification.AddNotification("Invoice", "Nota fiscal não encontrada");
                return new ResponseRemoveInvoiceToCartViewModel();
            }

            if (invoiceEntity.CarrinhoId is null)
            {
                domainNotification.AddNotification("Cart", "Esta nota não está em nenhum carrinho");
                return new ResponseRemoveInvoiceToCartViewModel();
            }

            var cartEntity = await cartRepository.GetByIdAsync(invoiceEntity.CarrinhoId.Value);
            if (cartEntity is null)
            {
                domainNotification.AddNotification("Cart", "Carrinho não encontrado");
                return new ResponseRemoveInvoiceToCartViewModel();
            }

            var success = await RemoveFromCartAsync(invoiceEntity, cartEntity);
            if (!success)
                return new ResponseRemoveInvoiceToCartViewModel();

            await unitOfWork.CommitAsync();
            return new ResponseRemoveInvoiceToCartViewModel
            {
                TotalBruto = cartEntity.TotalBruto,
                TotalLiquido = cartEntity.TotalLiquido
            };
        }

        private async Task<bool> RemoveFromCartAsync(InvoiceEntity invoice, CartEntity cart)
        {
            if (invoice.ValorLiquido is null)
            {
                domainNotification.AddNotification("Invoice", "Valor líquido não encontrado para a nota.");
                return false;
            }

            cart.TotalBruto -= invoice.ValorBruto;
            cart.TotalLiquido -= invoice.ValorLiquido.Value;

            cart.TotalBruto = Math.Max(cart.TotalBruto, 0);
            cart.TotalLiquido = Math.Max(cart.TotalLiquido, 0);

            invoice.CarrinhoId = null;
            invoice.ValorLiquido = null;

            invoiceRepository.Update(invoice);

            cartRepository.Update(cart);

            return true;
        }
    }
}
