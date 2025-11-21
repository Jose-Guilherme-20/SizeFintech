
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
    }
}
