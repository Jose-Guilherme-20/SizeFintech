using System.Threading.Tasks;
using SizeFintech.Application.Validations.Recebivel.InsertCompany;
using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Request;
using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Response;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;

namespace SizeFintech.Application.UseCases.Recebivel.InsertCompany
{
    public class InsertCompanyUseCase
        ( ICompanyRepository CompanyRepository,
          IDomainNotification domainNotification,
         IUnitOfWork unitOfWork
        ) 
        : IInsertCompanyUseCase
    {

        public async Task<ResponseInsertCompanyViewModel> ExecuteAsync(RequestInsertCompanyViewModel request)
        {
            if (! await Validation(request))
                return new ResponseInsertCompanyViewModel { };

            var companyEntity = request.ToEntity();
            companyEntity.CalcularLimiteAntecipacao();

            await CompanyRepository.AddAsync(companyEntity);

            await unitOfWork.CommitAsync();

            return new ResponseInsertCompanyViewModel 
            {
                Id = companyEntity.Id
            };
        }

        public async Task<bool> Validation(RequestInsertCompanyViewModel request)
        {
            if(! await CompanyExistsAsync(request.Cnpj))
                return false;

            var validate = new InsertCompanyValidation();

            var result = validate.Validate(request);

            if (!result.IsValid)
            {
                domainNotification.AddNotifications(result);
                return false ;
            }

            return true;
        }

        public async Task<bool> CompanyExistsAsync(string cnpj)
        {
            var companyExists = await CompanyRepository.GetByCnpjAsync(cnpj);
            if (companyExists != null)
            {
                domainNotification.AddNotification("Cnpj", "Já existe uma empresa cadastrada com este CNPJ.");
                return true;
            }
            return false;
        }
    }
}
