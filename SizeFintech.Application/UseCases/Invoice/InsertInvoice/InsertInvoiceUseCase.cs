using System.Threading.Tasks;
using SizeFintech.Application.Validations.Invoice.InsertInvoice;
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Request;
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Response;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;
using SizeFintech.Domain.Models.Notification;
using SizeFintech.Infra.Repository;

namespace SizeFintech.Application.UseCases.Invoice.InsertInvoice
{
    public class InsertInvoiceUseCase
        (
         IInvoiceRepository invoiceRepository,
         ICompanyRepository companyRepository,
         IDomainNotification domainNotification,
         IUnitOfWork unitOfWork
        ) : IInsertInvoiceUseCase
    {
        public async Task<ResponseInsertInvoiceViewModel> ExecuteAsync(RequestInsertInvoiceViewModel request)
        {
            if (!await Validate(request))
                return new ResponseInsertInvoiceViewModel { };

            var invoiceEntity = request.ToInvoiceEntity();

            await invoiceRepository.AddAsync(invoiceEntity);
            await unitOfWork.CommitAsync();

            return new ResponseInsertInvoiceViewModel
            {
                Id = invoiceEntity.Id
            };
        }

        public async Task<bool> Validate(RequestInsertInvoiceViewModel request)
        {
            if (!await CompanyExists(request.EmpresaId))
                return false;

            if (!await ValidateInvoiceNumber(request))
                return false;

            var validate = new InsertInvoiceValidation();

            var result = validate.Validate(request);
            if (!result.IsValid)
            {
                domainNotification.AddNotifications(result);
                return false;
            }

            return true;
        }

        public async Task<bool> CompanyExists(int companyId)
        {
            var company = await companyRepository.GetByIdAsync(companyId);

            if (company is null)
            {
                domainNotification.AddNotification("Empresa", "Empresa não encontrada");
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateInvoiceNumber(RequestInsertInvoiceViewModel request)
        {
            var invoice = await invoiceRepository.GetByParamsAsync(i => i.Numero == request.Numero);

            if (invoice is not null)
            {
                domainNotification.AddNotification("NotaFiscal", $"Já existe uma nota fiscal cadastrada com esse numero {request.Numero}");
                return false;
            }

            return true;
        }
    }
}
