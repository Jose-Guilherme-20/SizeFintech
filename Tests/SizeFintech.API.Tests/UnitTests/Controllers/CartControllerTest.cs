using Microsoft.AspNetCore.Mvc;
using Moq;
using SizeFintech.API.Controllers;
using SizeFintech.Application.UseCases.Cart.InsertInvoiceToCart;
using SizeFintech.Application.UseCases.Cart.RemoveInvoiceToCart;
using SizeFintech.Application.ViewModels.Cart.InsertInvoiceToCart.Response;
using SizeFintech.Application.ViewModels.Cart.RemoveInvoiceToCart.Response;

namespace SizeFintech.API.Tests.UnitTests.Controllers
{
    public class CartControllerTest
    {
        [Fact]
        public async Task InsertInvoiceToCartAsync_ShouldReturnCreatedResult_WithResponse()
        {
            // Arrange
            var mockUseCase = new Mock<IInsertInvoiceToCartUseCase>();
            var invoiceId = 1;
            var response = new ResponseInsertInvoiceToCartViewModel
            {
                IdCarrinho = 1,
                TotalBruto = 10000,
                TotalLiquido = 9555.66m
            };

            mockUseCase
                .Setup(x => x.ExecuteAsync(invoiceId))
                .ReturnsAsync(response);

            var controller = new CartController();

            // Act
            var result = await controller.InsertInvoiceToCartAsync(mockUseCase.Object, invoiceId);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            var model = Assert.IsType<ResponseInsertInvoiceToCartViewModel>(createdResult.Value);
            Assert.Equal(response.IdCarrinho, model.IdCarrinho);
            Assert.Equal(response.TotalBruto, model.TotalBruto);
            Assert.Equal(response.TotalLiquido, model.TotalLiquido);
        }

        [Fact]
        public async Task RemoveInvoiceFromCartAsync_ShouldReturnOkResult_WithResponse()
        {
            // Arrange
            var mockUseCase = new Mock<IRemoveInvoiceToCartUSeCase>();
            var invoiceId = 1;
            var response = new ResponseRemoveInvoiceToCartViewModel
            {
                TotalBruto = 5000,
                TotalLiquido = 4777.83m
            };

            mockUseCase
                .Setup(x => x.ExecuteAsync(invoiceId))
                .ReturnsAsync(response);

            var controller = new CartController();

            // Act
            var result = await controller.RemoveInvoiceFromCartAsync(mockUseCase.Object, invoiceId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<ResponseRemoveInvoiceToCartViewModel>(okResult.Value);
            Assert.Equal(response.TotalBruto, model.TotalBruto);
            Assert.Equal(response.TotalLiquido, model.TotalLiquido);
        }
    }
}
