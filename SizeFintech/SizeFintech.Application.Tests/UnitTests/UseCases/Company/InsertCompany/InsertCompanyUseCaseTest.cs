using System.Threading.Tasks;
using Moq;
using Xunit;
using SizeFintech.Application.UseCases.Company.InsertCompany;
using SizeFintech.Application.ViewModels.Company.InsertCompany.Request;
using SizeFintech.Application.ViewModels.Company.InsertCompany.Response;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Application.Tests.UnitTests.UseCases.Company.InsertCompany
{
    public class InsertCompanyUseCaseTest
    {
        private readonly Mock<ICompanyRepository> mockCompanyRepo;
        private readonly Mock<IDomainNotification> mockDomainNotification;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly InsertCompanyUseCase useCase;

        public InsertCompanyUseCaseTest()
        {
            mockCompanyRepo = new Mock<ICompanyRepository>();
            mockDomainNotification = new Mock<IDomainNotification>();
            mockUnitOfWork = new Mock<IUnitOfWork>();

            useCase = new InsertCompanyUseCase(
                mockCompanyRepo.Object,
                mockDomainNotification.Object,
                mockUnitOfWork.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmpty_WhenCompanyAlreadyExists()
        {
            // Arrange
            var request = new RequestInsertCompanyViewModel
            {
                Nome = "Empresa Teste",
                Cnpj = "12345678000199",
                Ramo = Domain.Models.Enums.RamoEnum.Produtos,
                Faturamento = 50000
            };

            mockCompanyRepo.Setup(x => x.GetByCnpjAsync(request.Cnpj))
                           .ReturnsAsync(new CompanyEntity {
                               Nome = "Empresa Teste",
                               Cnpj = "12345678000199"
                           });

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
            mockDomainNotification.Verify(x => x.AddNotification(
                "Cnpj", "Já existe uma empresa cadastrada com este CNPJ."), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmpty_WhenValidationFails()
        {
            // Arrange
            var request = new RequestInsertCompanyViewModel
            {
                Nome = "",
                Cnpj = "invalid-cnpj",
                Ramo = Domain.Models.Enums.RamoEnum.Servicos,
                Faturamento = 0
            };

            mockCompanyRepo.Setup(x => x.GetByCnpjAsync(It.IsAny<string>()))
                           .ReturnsAsync((CompanyEntity?)null);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
            mockDomainNotification.Verify(x => x.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldInsertCompany_WhenValidationPassesAndCompanyDoesNotExist()
        {
            // Arrange
            var request = new RequestInsertCompanyViewModel
            {
                Nome = "Empresa Teste",
                Cnpj = "60871231000100",
                Ramo = Domain.Models.Enums.RamoEnum.Produtos,
                Faturamento = 50000
            };

            mockCompanyRepo.Setup(x => x.GetByCnpjAsync(request.Cnpj))
                           .ReturnsAsync((CompanyEntity?)null);

            mockCompanyRepo.Setup(x => x.AddAsync(It.IsAny<CompanyEntity>()))
                           .Returns(Task.CompletedTask);

            mockUnitOfWork.Setup(x => x.CommitAsync())
                          .ReturnsAsync(1);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id >= 0); 
            mockCompanyRepo.Verify(x => x.AddAsync(It.IsAny<CompanyEntity>()), Times.Once);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
