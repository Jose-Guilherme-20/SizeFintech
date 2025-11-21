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
                (double)(1 + Taxa.TAXANTECIPACAOMENSAL),
                prazoDias / 30.0
            );

            var desagio = ValorBruto / (decimal)fator;

            var valorLiquido = ValorBruto - desagio;

            ValorLiquido = Math.Round(valorLiquido, 2);

        }
    }
}
