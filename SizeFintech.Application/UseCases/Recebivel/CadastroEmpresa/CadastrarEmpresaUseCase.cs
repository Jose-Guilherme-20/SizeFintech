using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Request;
using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Response;
using SizeFintech.Domain.Interfaces.UnitOfWork;

namespace SizeFintech.Application.UseCases.Recebivel.CadastroEmpresa
{
    public class CadastrarEmpresaUseCase(IUnitOfWork unitOfWork) : ICadastrarEmpresaUseCase
    {

        public async Task<ResponseCadastrarEmpresaViewModel> ExecuteAsync(RequestCadastrarEmpresaViewModel request)
        {

            return new ResponseCadastrarEmpresaViewModel { };
        }
    }
}
