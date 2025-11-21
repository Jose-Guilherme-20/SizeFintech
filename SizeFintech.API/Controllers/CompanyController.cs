using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SizeFintech.Application.UseCases.Recebivel.InsertCompany;
using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Request;
using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Response;

namespace SizeFintech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        /// <summary>
        /// Cria uma nova empresa no sistema.
        /// </summary>
        /// <param name="useCase">Use case responsável por executar o fluxo para criar empresas.</param>
        /// <param name="request">Parâmetros necessários para criar uma empresa.</param>
        /// <returns>Retorna o Id da nova empresa.</returns>
        [ProducesResponseType(typeof(ResponseInsertCompanyViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<ResponseInsertCompanyViewModel>> InsertCompanyAsync([FromServices] IInsertCompanyUseCase useCase ,[FromBody] RequestInsertCompanyViewModel request)
        {
            return Created(string.Empty, await useCase.ExecuteAsync(request));
        }
    }
}
