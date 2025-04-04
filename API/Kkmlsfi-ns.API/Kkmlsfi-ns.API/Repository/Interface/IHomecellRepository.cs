using Kkmlsfi_ns.API.Models.Domain;

namespace Kkmlsfi_ns.API.Repository.Interface
{
    public interface IHomecellRepository
    {
        Task<IEnumerable<Homecell>> GetAllHomecellsAsync(int? pageNumber = 1, int? pageSize = 100);
        Task<Homecell?> GetHomecellByIdAsync(int homecellId);
        Task<Homecell> CreateAsync(Homecell homecell);
    }
}
