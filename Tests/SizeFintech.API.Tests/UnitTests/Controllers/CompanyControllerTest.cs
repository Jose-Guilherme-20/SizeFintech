using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SizeFintech.API.Controllers;
using SizeFintech.Application.UseCases.Company.GetCompanyByCnpj;
using SizeFintech.Application.UseCases.Company.InsertCompany;
using SizeFintech.Application.UseCases.Company.UpdateCompany;
using SizeFintech.Application.ViewModels.Company.GetCompanyByCnpj.Response;
using SizeFintech.Application.ViewModels.Company.InsertCompany.Request;
using SizeFintech.Application.ViewModels.Company.InsertCompany.Response;
using SizeFintech.Application.ViewModels.Company.UpdateCompany.Request;
using Xunit;

namespace SizeFintech.Tests.Controllers
{
    public class CompanyControllerTest
    {
        [Fact]
        public async Task GetCompanyByCnpjAsync_ShouldReturnOk_WithCompany()
        {
            // Arrange
            var mockUseCase = new Mock<IGetCompanyByCnpjUseCase>();
            var cnpj = "73860960000103";
            var response = new ResponseCompanyByCnpjViewModel
            {
                Empresa = "Empresa Teste",
                Cnpj = cnpj,
                Limite = 50000
            };

            mockUseCase.Setup(x => x.ExecuteAsync(cnpj))
                       .ReturnsAsync(response);

            var controller = new CompanyController();

            // Act
            var result = await controller.GetCompanyByCnpjAsync(mockUseCase.Object, cnpj);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<ResponseCompanyByCnpjViewModel>(okResult.Value);
            Assert.Equal(cnpj, model.Cnpj);
            Assert.Equal("Empresa Teste", model.Empresa);
            Assert.Equal(50000, model.Limite);
        }

        [Fact]
        public async Task InsertCompanyAsync_ShouldReturnCreated_WithResponse()
        {
            // Arrange
            var mockUseCase = new Mock<IInsertCompanyUseCase>();
            var request = new RequestInsertCompanyViewModel
            {
                Nome = "Empresa Teste",
                Cnpj = "73860960000103",
                Faturamento = 50000
            };
            var response = new ResponseInsertCompanyViewModel
            {
                Id = 1
            };

            mockUseCase.Setup(x => x.ExecuteAsync(request))
                       .ReturnsAsync(response);

            var controller = new CompanyController();

            // Act
            var result = await controller.InsertCompanyAsync(mockUseCase.Object, request);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            var model = Assert.IsType<ResponseInsertCompanyViewModel>(createdResult.Value);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task UpdateCompanyAsync_ShouldReturnNoContent()
        {
            // Arrange
            var mockUseCase = new Mock<IUpdateCompanyUseCase>();
            var request = new RequestUpdateCompanyViewModel
            {
                Nome = "Empresa Atualizada",
                Faturamento = 60000
            };
            var id = 1;

            mockUseCase.Setup(x => x.ExecuteAsync(id, request))
                       .Returns(Task.CompletedTask);

            var controller = new CompanyController();

            // Act
            var result = await controller.UpdateCompanyAsync(mockUseCase.Object, id, request);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }
    }
}
