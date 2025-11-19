
namespace SizeFintech.Domain.Models.Entities
{
    public class NotaFiscalEntity : BaseEntity
    {
        public decimal ValorBruto { get; set; }
        public decimal? ValorLiquido { get; set; }
        public DateTime DataVencimento { get; set; }
        public int EmpresaId { get; set; }
        public int? CarrinhoId { get; set; }
        public CarrinhoEntity? Carrinho { get; set; }
        public EmpresaEntity Empresa { get; set; }
    }
}
