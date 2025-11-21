using SizeFintech.Domain.Models.Consts;

namespace SizeFintech.Domain.Models.Entities
{
    public class InvoiceEntity : BaseEntity
    {
        public int Numero { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal? ValorLiquido { get; set; }
        public DateTime DataVencimento { get; set; }
        public int EmpresaId { get; set; }
        public int? CarrinhoId { get; set; }
        public CartEntity? Carrinho { get; set; }
        public CompanyEntity Empresa { get; set; }

        public void AdvancePaymentCalculation()
        {
            var prazoDias = (DataVencimento.Date - DateTime.Now.Date).TotalDays;

            var fator = Math.Pow(
                1 + (double)Taxa.TAXANTECIPACAOMENSAL,
                prazoDias / 30.0
            );

            var valorLiquido = ValorBruto / (decimal)fator;

            var desagio = ValorBruto - valorLiquido;

            ValorLiquido = Math.Round(valorLiquido, 2);

        }
    }
}
