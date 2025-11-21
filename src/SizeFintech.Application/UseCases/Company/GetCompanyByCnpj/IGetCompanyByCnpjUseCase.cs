
using SizeFintech.Application.ViewModels.Company.GetCompanyByCnpj.Response;

namespace SizeFintech.Application.UseCases.Company.GetCompanyByCnpj
{
    public interface IGetCompanyByCnpjUseCase
    {
        Task<ResponseCompanyByCnpjViewModel> ExecuteAsync(string cnpj);
    }
}
