
namespace SizeFintech.Domain.Models.Entities
{
    public class CarrinhoEntity : BaseEntity
    {
        public int NotaFiscalId { get; set; }

        public ICollection<NotaFiscalEntity> NotasFiscais { get; set; }
    }
}
