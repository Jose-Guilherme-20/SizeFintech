using System.Threading.Tasks;
using Moq;
using Xunit;
using SizeFintech.Application.UseCases.Invoice.RemoveInvoice;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Interfaces.UnitOfWork;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Application.Tests.UseCases.Invoice
{
    public class RemoveInvoiceUseCaseTest
    {
        private readonly Mock<IInvoiceRepository> mockInvoiceRepo;
        private readonly Mock<IDomainNotification> mockDomainNotification;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly RemoveInvoiceUseCase useCase;

        public RemoveInvoiceUseCaseTest()
        {
            mockInvoiceRepo = new Mock<IInvoiceRepository>();
            mockDomainNotification = new Mock<IDomainNotification>();
            mockUnitOfWork = new Mock<IUnitOfWork>();

            useCase = new RemoveInvoiceUseCase(
                mockInvoiceRepo.Object,
                mockDomainNotification.Object,
                mockUnitOfWork.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenInvoiceNotFound()
        {
            // Arrange
            int invoiceId = 1;
            mockInvoiceRepo.Setup(x => x.GetByIdAsync(invoiceId))
                           .ReturnsAsync((InvoiceEntity?)null);

            // Act
            await useCase.ExecuteAsync(invoiceId);

            // Assert
            mockDomainNotification.Verify(x => x.AddNotification("Invoice", "Fatura não encontrada."), Times.Once);
            mockInvoiceRepo.Verify(x => x.Remove(It.IsAny<InvoiceEntity>()), Times.Never);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldRemoveInvoice_WhenInvoiceExists()
        {
            // Arrange
            int invoiceId = 1;
            var invoice = new InvoiceEntity { Id = invoiceId };
            mockInvoiceRepo.Setup(x => x.GetByIdAsync(invoiceId))
                           .ReturnsAsync(invoice);

            mockUnitOfWork.Setup(x => x.CommitAsync())
                          .ReturnsAsync(1);

            // Act
            await useCase.ExecuteAsync(invoiceId);

            // Assert
            mockInvoiceRepo.Verify(x => x.Remove(invoice), Times.Once);
            mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            mockDomainNotification.Verify(x => x.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
