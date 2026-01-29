using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SizeFintech.Application.UseCases.Company.GetCompanyByCnpj;
using SizeFintech.Application.UseCases.Company.InsertCompany;
using SizeFintech.Application.UseCases.Company.UpdateCompany;
using SizeFintech.Application.ViewModels.Company.GetCompanyByCnpj.Response;
using SizeFintech.Application.ViewModels.Company.InsertCompany.Request;
using SizeFintech.Application.ViewModels.Company.InsertCompany.Response;
using SizeFintech.Application.ViewModels.Company.UpdateCompany.Request;

namespace SizeFintech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        /// <summary>
        /// Obtém os detalhes de uma empresa com base no CNPJ fornecido.
        /// </summary>
        /// <param name="useCase">Responsável por buscar detalhes de um determinado cnpj.</param>
        /// <param name="cnpj">Identificador de uma empresa.</param>
        /// <returns>Retorna detalhes de uma empresa e suas notas fiscais que estão no carrinho.</returns>
        [ProducesResponseType(typeof(ResponseCompanyByCnpjViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<ResponseCompanyByCnpjViewModel>> GetCompanyByCnpjAsync([FromServices] IGetCompanyByCnpjUseCase useCase ,[FromRoute] string cnpj)
        {
            return Ok(await useCase.ExecuteAsync(cnpj));
        }


        /// <summary>
        /// Cria uma nova empresa no sistema.
        /// </summary>
        /// <param name="useCase">Use case responsável por executar o fluxo para criar empresas.</param>
        /// <param name="request">Parâmetros necessários para criar uma empresa.</param>
        /// <returns>Retorna o Id da nova empresa.</returns>
        [ProducesResponseType(typeof(ResponseInsertCompanyViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<ResponseInsertCompanyViewModel>> InsertCompanyAsync([FromServices] IInsertCompanyUseCase useCase ,[FromBody] RequestInsertCompanyViewModel request)
        {
            return Created(string.Empty, await useCase.ExecuteAsync(request));
        }

        /// <summary>
        /// Atualiza uma empresa no sistema.
        /// </summary>
        /// <param name="useCase">Use case responsável por executar o fluxo para atualizar empresas.</param>
        /// <param name="request">Parâmetros necessários para atualizar uma empresa.</param>
        /// <param name="id">Parâmetros necessários para buscar uma empresa.</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseInsertCompanyViewModel>> UpdateCompanyAsync([FromServices] IUpdateCompanyUseCase useCase, [FromRoute] int id ,[FromBody] RequestUpdateCompanyViewModel request)
        {
            await useCase.ExecuteAsync(id, request);
            return NoContent();
        }
    }
}
