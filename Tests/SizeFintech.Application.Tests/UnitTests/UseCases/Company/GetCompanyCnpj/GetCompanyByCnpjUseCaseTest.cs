using System.Threading.Tasks;
using Moq;
using Xunit;
using SizeFintech.Application.UseCases.Company.GetCompanyByCnpj;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Application.ViewModels.Company.GetCompanyByCnpj.Response;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Application.Tests.UnitTests.UseCases.Company.GetCompanyCnpj
{
    public class GetCompanyByCnpjUseCaseTest
    {
        private readonly Mock<ICompanyRepository> mockCompanyRepo;
        private readonly Mock<IDomainNotification> mockDomainNotification;
        private readonly GetCompanyByCnpjUseCase useCase;

        public GetCompanyByCnpjUseCaseTest()
        {
            mockCompanyRepo = new Mock<ICompanyRepository>();
            mockDomainNotification = new Mock<IDomainNotification>();

            useCase = new GetCompanyByCnpjUseCase(
                mockCompanyRepo.Object,
                mockDomainNotification.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmpty_WhenCompanyNotFound()
        {
            // Arrange
            string cnpj = "12345678000199";
            mockCompanyRepo.Setup(x => x.GetByCnpjAsync(cnpj))
                           .ReturnsAsync((CompanyEntity?)null);

            // Act
            var result = await useCase.ExecuteAsync(cnpj);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Empresa);
            Assert.Null(result.Cnpj);
            mockDomainNotification.Verify(x => x.AddNotification(
                "Cnpj", "Empresa não encontrada para o CNPJ informado."), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnViewModel_WhenCompanyFound()
        {
            // Arrange
            string cnpj = "12345678000199";
            var companyEntity = new CompanyEntity
            {
                Id = 1,
                Nome = "Empresa Teste",
                Cnpj = cnpj,
                LimiteCredito = 50000
            };
            mockCompanyRepo.Setup(x => x.GetByCnpjAsync(cnpj))
                           .ReturnsAsync(companyEntity);

            // Act
            var result = await useCase.ExecuteAsync(cnpj);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Empresa Teste", result.Empresa);
            Assert.Equal(cnpj, result.Cnpj);
            Assert.Equal(50000, result.Limite);
            mockDomainNotification.Verify(x => x.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
