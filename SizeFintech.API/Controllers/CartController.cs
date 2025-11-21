using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SizeFintech.Application.UseCases.Cart.InsertInvoiceToCart;
using SizeFintech.Application.ViewModels.Cart.InsertInvoiceToCart.Response;

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
    }
}
