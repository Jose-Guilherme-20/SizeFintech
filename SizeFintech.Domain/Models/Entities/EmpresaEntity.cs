
using SizeFintech.Domain.Models.Enums;

namespace SizeFintech.Domain.Models.Entities
{
    public class EmpresaEntity : BaseEntity
    {
        public required string Nome { get; set; }
        public required string Cnpj { get; set; }
        public decimal Faturamento { get; set; }
        public int RamoId { get; set; }
        public decimal LimiteCredito { get; set; }
        public ICollection<NotaFiscalEntity>? NotasFiscais { get; set; } 
        public ICollection<CarrinhoEntity>? Carrinhos { get; set; } 
    }
}
