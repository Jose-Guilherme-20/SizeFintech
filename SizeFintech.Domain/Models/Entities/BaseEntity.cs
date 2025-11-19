
namespace SizeFintech.Domain.Models.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataExclusao { get; set; }
    }
}
