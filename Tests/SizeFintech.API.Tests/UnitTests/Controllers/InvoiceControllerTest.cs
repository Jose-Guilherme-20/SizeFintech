using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SizeFintech.API.Controllers;
using SizeFintech.Application.UseCases.Invoice.InsertInvoice;
using SizeFintech.Application.UseCases.Invoice.RemoveInvoice;
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Request;
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Response;
using Xunit;

namespace SizeFintech.Tests.Controllers
{
    public class InvoiceControllerTest
    {
        [Fact]
        public async Task InsertInvoiceAsync_ShouldReturnCreatedResult_WithResponse()
        {
            // Arrange
            var mockUseCase = new Mock<IInsertInvoiceUseCase>();
            var request = new RequestInsertInvoiceViewModel
            {
                Numero = 1234,
                ValorBruto = 10000,
                DataVencimento = System.DateTime.Now.AddDays(30),
                EmpresaId = 1
            };

            var response = new ResponseInsertInvoiceViewModel
            {
                Id = 1
            };

            mockUseCase.Setup(x => x.ExecuteAsync(request))
                       .ReturnsAsync(response);

            var controller = new InvoiceController();

            // Act
            var result = await controller.InsertInvoiceAsync(mockUseCase.Object, request);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            var model = Assert.IsType<ResponseInsertInvoiceViewModel>(createdResult.Value);
            Assert.Equal(response.Id, model.Id);
        }

        [Fact]
        public async Task DeleteInvoiceAsync_ShouldReturnNoContent()
        {
            // Arrange
            var mockUseCase = new Mock<IRemoveInvoiceUseCase>();
            var invoiceId = 1;

            mockUseCase.Setup(x => x.ExecuteAsync(invoiceId))
                       .Returns(Task.CompletedTask);

            var controller = new InvoiceController();

            // Act
            var result = await controller.DeleteInvoiceAsync(mockUseCase.Object, invoiceId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
