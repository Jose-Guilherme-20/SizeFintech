using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Request;
using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Response;

namespace SizeFintech.Application.UseCases.Recebivel.InsertCompany
{
    public interface IInsertCompanyUseCase
    {
        Task<ResponseInsertCompanyViewModel> ExecuteAsync(RequestInsertCompanyViewModel request);
    }
}
