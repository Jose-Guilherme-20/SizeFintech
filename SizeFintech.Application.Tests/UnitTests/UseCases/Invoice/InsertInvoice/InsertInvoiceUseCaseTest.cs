using System.Threading.Tasks;
using Moq;
using Xunit;
using SizeFintech.Application.UseCases.Invoice.InsertInvoice;
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Request;
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Response;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Application.Tests.UnitTests.UseCases.Invoice.InsertInvoice
{
    public class InsertInvoiceUseCaseTest
    {
        private readonly Mock<IInvoiceRepository> mockInvoiceRepo;
        private readonly Mock<ICompanyRepository> mockCompanyRepo;
        private readonly Mock<IDomainNotification> mockDomainNotification;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly InsertInvoiceUseCase useCase;

        public InsertInvoiceUseCaseTest()
        {
            mockInvoiceRepo = new Mock<IInvoiceRepository>();
            mockCompanyRepo = new Mock<ICompanyRepository>();
            mockDomainNotification = new Mock<IDomainNotification>();
            mockUnitOfWork = new Mock<IUnitOfWork>();

            useCase = new InsertInvoiceUseCase(
                mockInvoiceRepo.Object,
                mockCompanyRepo.Object,
                mockDomainNotification.Object,
                mockUnitOfWork.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmpty_WhenCompanyDoesNotExist()
        {
            // Arrange
            var request = new RequestInsertInvoiceViewModel
            {
                Numero = 1234,
                ValorBruto = 1000,
                DataVencimento = DateTime.Now.AddDays(10),
                EmpresaId = 1
            };

            mockCompanyRepo.Setup(x => x.GetByIdAsync(request.EmpresaId))
                           .ReturnsAsync((CompanyEntity?)null);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.Equal(0, result.Id);
            mockDomainNotification.Verify(x => x.AddNotification("Empresa", "Empresa não encontrada"), Times.Once);
            mockInvoiceRepo.Verify(x => x.AddAsync(It.IsAny<InvoiceEntity>()), Times.Never);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmpty_WhenInvoiceNumberAlreadyExists()
        {
            // Arrange
            var request = new RequestInsertInvoiceViewModel
            {
                Numero = 1234,
                ValorBruto = 1000,
                DataVencimento = DateTime.Now.AddDays(10),
                EmpresaId = 1
            };

            mockCompanyRepo.Setup(x => x.GetByIdAsync(request.EmpresaId))
                           .ReturnsAsync(new CompanyEntity
                           {
                               Id = 1,
                               Cnpj = "60871231000100",
                               Nome = "Empresa Teste",
                           });

            mockInvoiceRepo.Setup(x => x.GetByParamsAsync(It.IsAny<System.Linq.Expressions.Expression<Func<InvoiceEntity, bool>>>()))
                           .ReturnsAsync(new InvoiceEntity { Id = 99 });

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.Equal(0, result.Id);
            mockDomainNotification.Verify(x => x.AddNotification("NotaFiscal", $"Já existe uma nota fiscal cadastrada com esse numero {request.Numero}"), Times.Once);
            mockInvoiceRepo.Verify(x => x.AddAsync(It.IsAny<InvoiceEntity>()), Times.Never);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldInsertInvoice_WhenValid()
        {
            // Arrange
            var request = new RequestInsertInvoiceViewModel
            {
                Numero = 1234,
                ValorBruto = 1000,
                DataVencimento = DateTime.Now.AddDays(10),
                EmpresaId = 1
            };

            mockCompanyRepo.Setup(x => x.GetByIdAsync(request.EmpresaId))
                           .ReturnsAsync(new CompanyEntity
                           {
                               Id = 1,
                               Cnpj = "60871231000100",
                               Nome = "Empresa Teste",
                           });

            mockInvoiceRepo.Setup(x => x.GetByParamsAsync(It.IsAny<System.Linq.Expressions.Expression<Func<InvoiceEntity, bool>>>()))
                           .ReturnsAsync((InvoiceEntity?)null);

            mockInvoiceRepo.Setup(x => x.AddAsync(It.IsAny<InvoiceEntity>()))
                           .Returns(Task.CompletedTask);

            mockUnitOfWork.Setup(x => x.CommitAsync())
                          .ReturnsAsync(1);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.True(result.Id >= 0);
            mockInvoiceRepo.Verify(x => x.AddAsync(It.IsAny<InvoiceEntity>()), Times.Once);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
