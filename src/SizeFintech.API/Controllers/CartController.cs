using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SizeFintech.Application.UseCases.Cart.InsertInvoiceToCart;
using SizeFintech.Application.UseCases.Cart.RemoveInvoiceToCart;
using SizeFintech.Application.ViewModels.Cart.InsertInvoiceToCart.Response;
using SizeFintech.Application.ViewModels.Cart.RemoveInvoiceToCart.Response;

namespace SizeFintech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        /// <summary>
        /// Inserir uma fatura (invoice) no carrinho (cart)
        /// </summary>
        /// <param name="useCase">Responsável por exececutar o fluxo para adicionar a nota ao carrinho</param>
        /// <param name="invoiceId">Identificador da nota fiscal</param>
        /// <returns>Retorna dados do carrinho.</returns>
        [ProducesResponseType(typeof(ResponseInsertInvoiceToCartViewModel),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{invoiceId}")]
        public async Task<ActionResult<ResponseInsertInvoiceToCartViewModel>> InsertInvoiceToCartAsync([FromServices] IInsertInvoiceToCartUseCase useCase, [FromRoute] int invoiceId)
        {
            return Created(string.Empty, await useCase.ExecuteAsync(invoiceId));
        }

        /// <summary>
        /// Remove uma fatura (invoice) do carrinho (cart)
        /// </summary>
        /// <param name="useCase">Responsável por executar o fluxo para remover a nota do carrinho</param>
        /// <param name="invoiceId">Identificador da nota fiscal</param>
        /// <returns>Retorna os dados atualizados do carrinho.</returns>
        [ProducesResponseType(typeof(ResponseRemoveInvoiceToCartViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{invoiceId}")]
        public async Task<ActionResult<ResponseRemoveInvoiceToCartViewModel>> RemoveInvoiceFromCartAsync(
            [FromServices] IRemoveInvoiceToCartUSeCase useCase,
            [FromRoute] int invoiceId)
        {
            var result = await useCase.ExecuteAsync(invoiceId);

            return Ok(result);
        }

    }
}
