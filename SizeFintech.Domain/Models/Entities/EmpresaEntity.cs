
using SizeFintech.Domain.Models.Enums;

namespace SizeFintech.Domain.Models.Entities
{
    public class EmpresaEntity : BaseEntity
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public decimal Faturamento { get; set; }
        public int RamoId { get; set; }
        public decimal LimiteAntecipacao { get; set; }
    }
}
