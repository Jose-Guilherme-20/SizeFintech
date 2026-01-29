
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Models.Entities;
using SizeFintech.Infra.Context;

namespace SizeFintech.Infra.Repository
{
    public class CartRepository : EntityBaseRepository<CartEntity>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }
    }
}
