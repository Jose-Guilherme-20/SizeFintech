
namespace SizeFintech.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
