
using SizeFintech.Infra.Context;

namespace SizeFintech.Infra.Repository
{
    public class EmpresaRepository : EntityBaseRepository<Domain.Models.Entities.EmpresaEntity>, Domain.Interfaces.Repository.IEmpresaRepository
    {
        public EmpresaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
