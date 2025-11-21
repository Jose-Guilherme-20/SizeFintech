
namespace SizeFintech.Domain.Interfaces.Repository
{
    public interface ICompanyRepository : IEntityBaseRepository<Models.Entities.CompanyEntity>
    {
        Task<Domain.Models.Entities.CompanyEntity?> GetByCnpjAsync(string cnpj);
    }
}
