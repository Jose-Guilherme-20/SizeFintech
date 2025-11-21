
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Models.Entities;
using SizeFintech.Infra.Context;

namespace SizeFintech.Infra.Repository
{
    public class CarrinhoRepository : EntityBaseRepository<CarrinhoEntity>, ICarrinhoRepository
    {
        public CarrinhoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
