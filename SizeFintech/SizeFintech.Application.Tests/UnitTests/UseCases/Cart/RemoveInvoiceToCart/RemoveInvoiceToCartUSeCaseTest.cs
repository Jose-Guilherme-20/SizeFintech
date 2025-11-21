using System.Threading.Tasks;
using Moq;
using Xunit;
using SizeFintech.Application.UseCases.Cart.RemoveInvoiceToCart;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;
using SizeFintech.Domain.Models.Entities;
using SizeFintech.Application.ViewModels.Cart.RemoveInvoiceToCart.Response;

namespace SizeFintech.Application.Tests.UnitTests.UseCases.Cart.RemoveInvoiceToCart
{
    public class RemoveInvoiceToCartUseCaseTest
    {
        private readonly Mock<IInvoiceRepository> mockInvoiceRepo;
        private readonly Mock<ICartRepository> mockCartRepo;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IDomainNotification> mockDomainNotification;
        private readonly RemoveInvoiceToCartUSeCase useCase;

        public RemoveInvoiceToCartUseCaseTest()
        {
            mockInvoiceRepo = new Mock<IInvoiceRepository>();
            mockCartRepo = new Mock<ICartRepository>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockDomainNotification = new Mock<IDomainNotification>();

            useCase = new RemoveInvoiceToCartUSeCase(
                mockInvoiceRepo.Object,
                mockCartRepo.Object,
                mockUnitOfWork.Object,
                mockDomainNotification.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmpty_WhenInvoiceNotFound()
        {
            // Arrange
            mockInvoiceRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                           .ReturnsAsync((InvoiceEntity?)null);

            // Act
            var result = await useCase.ExecuteAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TotalBruto);
            Assert.Equal(0, result.TotalLiquido);
            mockDomainNotification.Verify(x => x.AddNotification("Invoice", "Nota fiscal não encontrada"), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmpty_WhenInvoiceHasNoCart()
        {
            // Arrange
            var invoice = new InvoiceEntity { Id = 1, CarrinhoId = null };
            mockInvoiceRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(invoice);

            // Act
            var result = await useCase.ExecuteAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TotalBruto);
            Assert.Equal(0, result.TotalLiquido);
            mockDomainNotification.Verify(x => x.AddNotification("Cart", "Esta nota não está em nenhum carrinho"), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmpty_WhenCartNotFound()
        {
            // Arrange
            var invoice = new InvoiceEntity { Id = 1, CarrinhoId = 2 };
            mockInvoiceRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(invoice);
            mockCartRepo.Setup(x => x.GetByIdAsync(2)).ReturnsAsync((CartEntity?)null);

            // Act
            var result = await useCase.ExecuteAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TotalBruto);
            Assert.Equal(0, result.TotalLiquido);
            mockDomainNotification.Verify(x => x.AddNotification("Cart", "Carrinho não encontrado"), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldRemoveInvoiceFromCartSuccessfully()
        {
            // Arrange
            var invoice = new InvoiceEntity { Id = 1, CarrinhoId = 2, ValorBruto = 1000, ValorLiquido = 950 };
            var cart = new CartEntity { Id = 2, TotalBruto = 2000, TotalLiquido = 1900 };
            mockInvoiceRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(invoice);
            mockCartRepo.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(cart);
            mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            // Act
            var result = await useCase.ExecuteAsync(1);

            // Assert
            Assert.Equal(1000, cart.TotalBruto);
            Assert.Equal(950, cart.TotalLiquido);
            Assert.Null(invoice.CarrinhoId);
            Assert.Null(invoice.ValorLiquido);
            Assert.Equal(cart.TotalBruto, result.TotalBruto);
            Assert.Equal(cart.TotalLiquido, result.TotalLiquido);

            mockInvoiceRepo.Verify(x => x.Update(invoice), Times.Once);
            mockCartRepo.Verify(x => x.Update(cart), Times.Once);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
