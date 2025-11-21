
using Microsoft.EntityFrameworkCore;
using SizeFintech.Infra.Context;

namespace SizeFintech.Infra.Repository
{
    public class CompanyRepository : EntityBaseRepository<Domain.Models.Entities.CompanyEntity>, Domain.Interfaces.Repository.ICompanyRepository
    {
        private readonly AppDbContext _context;
        public CompanyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Domain.Models.Entities.CompanyEntity?> GetByCnpjAsync(string cnpj)
        {
            return await _context.Set<Domain.Models.Entities.CompanyEntity>()
                .FirstOrDefaultAsync(e => e.Cnpj == cnpj);
        }
    }
}
