
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Models.Entities;
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
            return await _context.Set<CompanyEntity>()
                .Include(c => c.Carrinho)
                .Include(c => c.NotasFiscais.Where(n => n.DataExclusao == null && n.CarrinhoId != null ))
                .FirstOrDefaultAsync(e => e.Cnpj == cnpj);
        }

    }
}
