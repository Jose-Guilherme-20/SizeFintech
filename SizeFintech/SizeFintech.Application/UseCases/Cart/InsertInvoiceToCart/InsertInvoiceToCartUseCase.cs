
using SizeFintech.Application.ViewModels.Cart.InsertInvoiceToCart.Response;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Application.UseCases.Cart.InsertInvoiceToCart
{
    public class InsertInvoiceToCartUseCase
        (
          ICartRepository cartRepository,
          IInvoiceRepository invoiceRepository,
          ICompanyRepository companyRepository,
          IDomainNotification domainNotification,
          IUnitOfWork unitOfWork
        ) : IInsertInvoiceToCartUseCase
    {

        public async Task<ResponseInsertInvoiceToCartViewModel> ExecuteAsync(int invoiceId)
        {
            var invoiceEntity = await invoiceRepository.GetByIdAsync(invoiceId);
            if (invoiceEntity is null)
            {
                domainNotification.AddNotification("Invoice", "Nota fiscal não encontrada");
                return new ResponseInsertInvoiceToCartViewModel();
            }

            invoiceEntity.AdvancePaymentCalculation();

            if (invoiceEntity.ValorLiquido is null)
            {
                domainNotification.AddNotification("Invoice", "Erro ao calcular valor líquido.");
                return new ResponseInsertInvoiceToCartViewModel();
            }

            var cartEntity = await AddOrUpdateCartAsync(invoiceEntity);

            if (cartEntity is null)
                return new ResponseInsertInvoiceToCartViewModel();

            invoiceEntity.Carrinho = cartEntity;
            invoiceRepository.Update(invoiceEntity);

            await unitOfWork.CommitAsync();

            return new ResponseInsertInvoiceToCartViewModel
            {
                IdCarrinho = cartEntity!.Id,
                TotalBruto = cartEntity.TotalBruto,
                TotalLiquido = cartEntity.TotalLiquido
            };
        }

        private async Task<CartEntity?> AddOrUpdateCartAsync(InvoiceEntity invoiceEntity)
        {
            var cartEntity = await cartRepository.GetByParamsAsync(
                c => c.EmpresaId == invoiceEntity.EmpresaId
            );

            if (cartEntity is null)
            {
                cartEntity = new CartEntity
                {
                    EmpresaId = invoiceEntity.EmpresaId,
                    TotalBruto = invoiceEntity.ValorBruto,
                    TotalLiquido = invoiceEntity.ValorLiquido!.Value
                };

            }
            else
            {
                cartEntity.TotalBruto += invoiceEntity.ValorBruto;
                cartEntity.TotalLiquido += invoiceEntity.ValorLiquido!.Value;
                
            }

            var company = await companyRepository.GetByIdAsync(invoiceEntity.EmpresaId);

            if (cartEntity.TotalBruto > company!.LimiteCredito)
            {
                domainNotification.AddNotification("Cart", "O limite de crédito da empresa foi excedido.");
                return null;
            }

            if (cartEntity.Id > 0)
                cartRepository.Update(cartEntity);
            else
                await cartRepository.AddAsync(cartEntity);

            return cartEntity;
        }
    }
}
