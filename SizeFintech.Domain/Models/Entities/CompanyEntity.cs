
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
        public ICollection<InvoiceEntity?> NotasFiscais { get; set; } 
        public CartEntity? Carrinho { get; set; }

        public void CalcularLimiteAntecipacao()
        {
            if (Faturamento >= 10000 && Faturamento <= 50000)
            {
                LimiteCredito =  Faturamento * 0.50m;
            }

            if (Faturamento >= 50001 && Faturamento <= 100000)
            {
                LimiteCredito = RamoId == (int)RamoEnum.Servicos
                    ? Faturamento * 0.55m
                    : Faturamento * 0.60m;
            }

            if (Faturamento > 100001)
            {
                LimiteCredito =  RamoId == (int)RamoEnum.Servicos
                    ? Faturamento * 0.60m
                    : Faturamento * 0.65m;
            }

            LimiteCredito =  0;
        }
    }
}
