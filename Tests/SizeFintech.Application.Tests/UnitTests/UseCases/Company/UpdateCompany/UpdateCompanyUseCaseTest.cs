using System.Threading.Tasks;
using Moq;
using Xunit;
using SizeFintech.Application.UseCases.Company.UpdateCompany;
using SizeFintech.Application.ViewModels.Company.UpdateCompany.Request;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;
using SizeFintech.Domain.Models.Entities;
using SizeFintech.Domain.Models.Enums;

namespace SizeFintech.Application.Tests.UseCases.Company
{
    public class UpdateCompanyUseCaseTest
    {
        private readonly Mock<ICompanyRepository> mockCompanyRepo;
        private readonly Mock<IDomainNotification> mockDomainNotification;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly UpdateCompanyUseCase useCase;

        public UpdateCompanyUseCaseTest()
        {
            mockCompanyRepo = new Mock<ICompanyRepository>();
            mockDomainNotification = new Mock<IDomainNotification>();
            mockUnitOfWork = new Mock<IUnitOfWork>();

            useCase = new UpdateCompanyUseCase(
                mockCompanyRepo.Object,
                mockDomainNotification.Object,
                mockUnitOfWork.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturn_WhenValidationFails()
        {
            // Arrange
            var request = new RequestUpdateCompanyViewModel
            {
                Nome = "", // inválido para falhar na validação
                Ramo = Domain.Models.Enums.RamoEnum.Servicos,
                Faturamento = 50000
            };

            // Act
            await useCase.ExecuteAsync(1, request);

            // Assert
            mockDomainNotification.Verify(x => x.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Once);
            mockCompanyRepo.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldNotify_WhenCompanyNotFound()
        {
            // Arrange
            var request = new RequestUpdateCompanyViewModel
            {
                Id = 2,
                Cnpj = "60871231000100",
                Nome = "Empresa Teste",
                Ramo = RamoEnum.Servicos,
                Faturamento = 50000
            };

            mockCompanyRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                           .ReturnsAsync((CompanyEntity?)null);

            // Act
            await useCase.ExecuteAsync(1, request);

            // Assert
            mockDomainNotification.Verify(x => x.AddNotification("Empresa", "Empresa não encontrada."), Times.Once);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldUpdateCompany_WhenValidationPassesAndCompanyExists()
        {
            // Arrange
            var request = new RequestUpdateCompanyViewModel
            {
                Id = 2,
                Cnpj = "60871231000100",
                Nome = "Empresa Atualizada",
                Ramo = RamoEnum.Produtos,
                Faturamento = 60000
            };

            var companyEntity = new CompanyEntity
            {
                Cnpj = "60871231000100",
                Id = 1,
                Nome = "Empresa Original",
                RamoId = (int)RamoEnum.Produtos,
                Faturamento = 50000
            };

            mockCompanyRepo.Setup(x => x.GetByIdAsync(1))
                           .ReturnsAsync(companyEntity);

            mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            // Act
            await useCase.ExecuteAsync(1, request);

            // Assert
            mockCompanyRepo.Verify(x => x.Update(It.IsAny<CompanyEntity>()), Times.Once);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
