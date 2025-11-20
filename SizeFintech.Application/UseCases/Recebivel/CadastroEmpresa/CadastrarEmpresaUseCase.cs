using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Request;
using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Response;

namespace SizeFintech.Application.UseCases.Recebivel.CadastroEmpresa
{
    public class CadastrarEmpresaUseCase : ICadastrarEmpresaUseCase
    {

        public async Task<ResponseCadastrarEmpresaViewModel> ExecuteAsync(RequestCadastrarEmpresaViewModel request)
        {

            return new ResponseCadastrarEmpresaViewModel { };
        }
    }
}
