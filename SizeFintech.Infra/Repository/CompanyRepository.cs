
using SizeFintech.Infra.Context;

namespace SizeFintech.Infra.Repository
{
    public class CompanyRepository : EntityBaseRepository<Domain.Models.Entities.CompanyEntity>, Domain.Interfaces.Repository.ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }
    }
}
