using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SizeFintech.Application.UseCases.Invoice.InsertInvoice;
using SizeFintech.Application.UseCases.Invoice.RemoveInvoice;
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Request;
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Response;

namespace SizeFintech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        /// <summary>
        /// Cria uma nova fatura (invoice)
        /// </summary>
        /// <param name="useCase">Responsável por executar fluxo para adicionar uma nota fiscal.</param>
        /// <param name="request">Parâmetros necessários para a criação de uma nota fiscal.</param>
        /// <returns>Retorna o identificador da nota criada.</returns>
        [ProducesResponseType(typeof(ResponseInsertInvoiceViewModel),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<ResponseInsertInvoiceViewModel>> InsertInvoiceAsync([FromServices] IInsertInvoiceUseCase useCase, [FromBody] RequestInsertInvoiceViewModel request)
        {
            return Created(string.Empty, await useCase.ExecuteAsync(request));
        }

        /// <summary>
        /// Exclui uma fatura (invoice) pelo seu identificador.
        /// </summary>
        /// <param name="useCase">Responsável por fazer o delete lógico da nota fiscal.</param>
        /// <param name="id">Identificador da nota.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInvoiceAsync([FromServices] IRemoveInvoiceUseCase useCase, [FromRoute] int id)
        {
            await useCase.ExecuteAsync(id);
            return NoContent();
        }
    }
}
