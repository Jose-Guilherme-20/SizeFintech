
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Request
{
    public class RequestInsertInvoiceViewModel
    {
        public int Numero { get; set; }
        public decimal ValorBruto { get; set; }
        public DateTime DataVencimento { get; set; }
        public int EmpresaId { get; set; }

        public InvoiceEntity ToInvoiceEntity()
        {
            return new InvoiceEntity
            {
                Numero = Numero,
                ValorBruto = ValorBruto,
                DataVencimento = DataVencimento,
                EmpresaId = EmpresaId
            };
        }
    }
}
