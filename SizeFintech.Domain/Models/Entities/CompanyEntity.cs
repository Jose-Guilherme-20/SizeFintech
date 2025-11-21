
using SizeFintech.Domain.Models.Enums;

namespace SizeFintech.Domain.Models.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public required string Nome { get; set; }
        public required string Cnpj { get; set; }
        public decimal Faturamento { get; set; }
        public int RamoId { get; set; }
        public decimal LimiteCredito { get; set; }
        public ICollection<InvoiceEntity>? NotasFiscais { get; set; } 
        public ICollection<CartEntity>? Carrinhos { get; set; } 
    }
}
