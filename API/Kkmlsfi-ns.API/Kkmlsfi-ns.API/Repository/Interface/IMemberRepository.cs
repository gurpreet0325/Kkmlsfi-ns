using Kkmlsfi_ns.API.Models.Domain;

namespace Kkmlsfi_ns.API.Repository.Interface
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllMembersAsync(string? searchFilter = null, 
                                                     string? sortBy = null, 
                                                     string? sortDirection = null,
                                                     int? pageNumber = 1,
                                                     int? pageSize = 100);
        Task<Member?> GetMemberByIdAsync(int memberId);
        Task<Member> CreateAsync(Member member);
        Task<Member?> UpdateAsync(Member member);
        Task<Member?> DeleteAsync(Member member);
        Task<int> GetTotalCount();
    }
}
