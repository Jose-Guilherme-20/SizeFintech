
namespace SizeFintech.Domain.Models.Entities
{
    public class CartEntity : BaseEntity
    {
        public decimal TotalBruto { get; set; }
        public decimal TotalLiquido { get; set; }
        public int EmpresaId { get; set; }
        public CompanyEntity Empresa { get; set; } 
        public ICollection<InvoiceEntity>? NotasFiscais { get; set; }
    }
}
