
namespace SizeFintech.Domain.Models.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Active = true;
        }
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
