
using SizeFintech.Application.ViewModels.Company.UpdateCompany.Request;

namespace SizeFintech.Application.UseCases.Company.UpdateCompany
{
    public interface IUpdateCompanyUseCase
    {
        Task ExecuteAsync(int id, RequestUpdateCompanyViewModel request);
    }
}
