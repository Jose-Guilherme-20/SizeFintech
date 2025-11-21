
using SizeFintech.Application.Validations.Company.UpdateCompany;
using SizeFintech.Application.ViewModels.Company.UpdateCompany.Request;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;

namespace SizeFintech.Application.UseCases.Company.UpdateCompany
{
    public class UpdateCompanyUseCase
        (
         ICompanyRepository companyRepository,
         IDomainNotification domainNotification,
         IUnitOfWork unitOfWork
        ) : IUpdateCompanyUseCase
    {
        public async Task ExecuteAsync(int id,RequestUpdateCompanyViewModel request)
        {
            if (!Validation(request))
                return;

            var companyEntity = await companyRepository.GetByIdAsync(id);

            if (companyEntity == null)
            {
                domainNotification.AddNotification("Empresa", "Empresa não encontrada.");
                return;
            }

            var companyUpdate = request.Update(companyEntity);
            companyUpdate.CalcularLimiteAntecipacao();

            companyRepository.Update(companyUpdate);
            await unitOfWork.CommitAsync();
        }

        public bool Validation(RequestUpdateCompanyViewModel request)
        {
            var validate = new UpdateCompanyValidation();

            var result = validate.Validate(request);

            if (!result.IsValid)
            {
                domainNotification.AddNotifications(result);
                return false;
            }

            return true;
        }

    }
}
