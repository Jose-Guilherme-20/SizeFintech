using SizeFintech.Application.ViewModels.Company.InsertCompany.Request;
using SizeFintech.Application.ViewModels.Company.InsertCompany.Response;

namespace SizeFintech.Application.UseCases.Company.InsertCompany
{
    public interface IInsertCompanyUseCase
    {
        Task<ResponseInsertCompanyViewModel> ExecuteAsync(RequestInsertCompanyViewModel request);
    }
}
