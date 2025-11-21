using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using SizeFintech.Application.UseCases.Cart.InsertInvoiceToCart;
using SizeFintech.Application.ViewModels.Cart.InsertInvoiceToCart.Response;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;
using SizeFintech.Domain.Models.Entities;
using Xunit;

namespace SizeFintech.Application.Tests.UnitTests.UseCases.Cart.InsertInvoiceToCart
{
    public class InsertInvoiceToCartUseCaseTest
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnResponse_WhenInvoiceExistsAndCartCreated()
        {
            // Arrange
            var invoice = new InvoiceEntity
            {
                Id = 1,
                ValorBruto = 10000,
                ValorLiquido = 9500,
                EmpresaId = 1,
                DataVencimento = DateTime.Now.AddDays(30)

            };

            var company = new CompanyEntity
            {
                Cnpj = "73860960000103",
                Nome = "Empresa Teste",
                Id = 1,
                LimiteCredito = 50000
            };

            var mockInvoiceRepo = new Mock<IInvoiceRepository>();
            var mockCartRepo = new Mock<ICartRepository>();
            var mockCompanyRepo = new Mock<ICompanyRepository>();
            var mockNotification = new Mock<IDomainNotification>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockInvoiceRepo.Setup(x => x.GetByIdAsync(invoice.Id))
                           .ReturnsAsync(invoice);
            mockCompanyRepo.Setup(x => x.GetByIdAsync(invoice.EmpresaId))
                           .ReturnsAsync(company);

            mockCartRepo.Setup(x => x.GetByParamsAsync(It.IsAny<Expression<Func<CartEntity, bool>>>()))
                        .ReturnsAsync((CartEntity?)null);
            mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            var useCase = new InsertInvoiceToCartUseCase(
                mockCartRepo.Object,
                mockInvoiceRepo.Object,
                mockCompanyRepo.Object,
                mockNotification.Object,
                mockUnitOfWork.Object
            );

            // Act
            var result = await useCase.ExecuteAsync(invoice.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(invoice.ValorBruto, result.TotalBruto);
            Assert.Equal(invoice.ValorLiquido, result.TotalLiquido);
            mockInvoiceRepo.Verify(x => x.Update(invoice), Times.Once);
            mockCartRepo.Verify(x => x.AddAsync(It.IsAny<CartEntity>()), Times.Once);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmptyResponse_WhenInvoiceNotFound()
        {
            // Arrange
            var mockInvoiceRepo = new Mock<IInvoiceRepository>();
            var mockCartRepo = new Mock<ICartRepository>();
            var mockCompanyRepo = new Mock<ICompanyRepository>();
            var mockNotification = new Mock<IDomainNotification>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockInvoiceRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                           .ReturnsAsync((InvoiceEntity?)null);

            var useCase = new InsertInvoiceToCartUseCase(
                mockCartRepo.Object,
                mockInvoiceRepo.Object,
                mockCompanyRepo.Object,
                mockNotification.Object,
                mockUnitOfWork.Object
            );

            // Act
            var result = await useCase.ExecuteAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TotalBruto);
            Assert.Equal(0, result.TotalLiquido);
            mockNotification.Verify(x => x.AddNotification("Invoice", "Nota fiscal não encontrada"), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnNull_WhenCartExceedsLimit()
        {
            // Arrange
            var invoice = new InvoiceEntity
            {
                Id = 1,
                ValorBruto = 60000,
                ValorLiquido = 57000,
                EmpresaId = 1,
                DataVencimento = DateTime.Now.AddDays(30)
            };

            var company = new CompanyEntity
            {
                Id = 1,
                Cnpj = "73860960000103",
                Nome = "Empresa Teste",
                LimiteCredito = 50000
            };

            var mockInvoiceRepo = new Mock<IInvoiceRepository>();
            var mockCartRepo = new Mock<ICartRepository>();
            var mockCompanyRepo = new Mock<ICompanyRepository>();
            var mockNotification = new Mock<IDomainNotification>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockInvoiceRepo.Setup(x => x.GetByIdAsync(invoice.Id))
                           .ReturnsAsync(invoice);
            mockCompanyRepo.Setup(x => x.GetByIdAsync(invoice.EmpresaId))
                           .ReturnsAsync(company);

            mockCartRepo.Setup(x => x.GetByParamsAsync(It.IsAny<Expression<Func<CartEntity, bool>>>()))
                        .ReturnsAsync((CartEntity?)null);

            var useCase = new InsertInvoiceToCartUseCase(
                mockCartRepo.Object,
                mockInvoiceRepo.Object,
                mockCompanyRepo.Object,
                mockNotification.Object,
                mockUnitOfWork.Object
            );

            // Act
            var result = await useCase.ExecuteAsync(invoice.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TotalBruto);
            Assert.Equal(0, result.TotalLiquido);
            mockNotification.Verify(x => x.AddNotification("Cart", "O limite de crédito da empresa foi excedido."), Times.Once);
        }
    }
}
