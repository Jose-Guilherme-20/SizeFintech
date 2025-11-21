
using SizeFintech.Application.ViewModels.Company.GetCompanyByCnpj.Response;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Infra.Repository;

namespace SizeFintech.Application.UseCases.Company.GetCompanyByCnpj
{
    public class GetCompanyByCnpjUseCase
        (
           ICompanyRepository companyRepository,
           IDomainNotification domainNotification
        ) : IGetCompanyByCnpjUseCase
    {
        public async Task<ResponseCompanyByCnpjViewModel> ExecuteAsync(string cnpj)
        {
            var companyEntity = await companyRepository.GetByCnpjAsync(cnpj);

            if (companyEntity is null)
            {
                domainNotification.AddNotification("Cnpj", "Empresa não encontrada para o CNPJ informado.");
                return new ResponseCompanyByCnpjViewModel();
            }
             
            return new ResponseCompanyByCnpjViewModel().ToViewModel(companyEntity);
        }
    }
}
