
namespace SizeFintech.Domain.Models.Entities
{
    public class CarrinhoEntity : BaseEntity
    {
        public decimal TotalBruto { get; set; }
        public decimal TotalLiquido { get; set; }
        public int EmpresaId { get; set; }
        public EmpresaEntity Empresa { get; set; } 
        public ICollection<NotaFiscalEntity>? NotasFiscais { get; set; }
    }
}
