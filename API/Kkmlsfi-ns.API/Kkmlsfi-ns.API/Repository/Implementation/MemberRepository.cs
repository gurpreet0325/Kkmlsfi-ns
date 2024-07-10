using Kkmlsfi_ns.API.Data;
using Kkmlsfi_ns.API.Models.Domain;
using Kkmlsfi_ns.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Kkmlsfi_ns.API.Repository.Implementation
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext dbContext;

        public MemberRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync(string? searchFilter = null, 
                                                                  string? sortBy = null, 
                                                                  string? sortDirection = null,
                                                                  int? pageNumber = 1,
                                                                  int? pageSize = 100)
        {
            var members = dbContext.Members.Where(m => !m.IsRemovedFromView).AsQueryable();

            //filter
            if (!string.IsNullOrWhiteSpace(searchFilter))
            {
                members = members.Where(m => m.FirstName.Contains(searchFilter) || m.LastName.Contains(searchFilter));
            }

            //sort
            var isAsc = string.Equals(sortDirection, "asc") ? true : false;
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("First Name"))
                {
                    members = isAsc ? members.OrderBy(m => m.FirstName).ThenBy(m => m.LastName) : members.OrderByDescending(m => m.FirstName).ThenBy(m => m.LastName);
                }
                else if (sortBy.Equals("Last Name"))
                {
                    members = isAsc ? members.OrderBy(m => m.LastName).ThenBy(m => m.FirstName) : members.OrderByDescending(m => m.LastName).ThenBy(m => m.FirstName);
                }
            }
            else
            {
                members = members.OrderBy(m => m.LastName).ThenBy(m => m.FirstName);
            }

            //paging
            var skipResults = (pageNumber - 1) * pageSize;
            members = members.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await members.ToListAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int memberId)
        {
            return await dbContext.Members.FirstOrDefaultAsync(m => m.MemberId == memberId);
        }

        public async Task<Member> CreateAsync(Member member)
        {
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            return member;
        }

        public async Task<Member?> UpdateAsync(Member member)
        {
            var existingMember = await dbContext.Members.FirstOrDefaultAsync(m => m.MemberId == member.MemberId);

            if (existingMember == null)
            {
                return null;
            }

            var entry = dbContext.Entry(existingMember);
            entry.CurrentValues.SetValues(member);
            entry.Property(m => m.InsertedBy).IsModified = false;
            entry.Property(m => m.InsertedDate).IsModified = false;
            entry.Property(m => m.IsRemovedFromView).IsModified = false;
            await dbContext.SaveChangesAsync();

            return member;
        }

        public async Task<Member?> DeleteAsync(Member member)
        {
            var existingMember = await dbContext.Members.FirstOrDefaultAsync(m => m.MemberId == member.MemberId);

            if (existingMember == null)
            {
                return null;
            }

            existingMember.IsRemovedFromView = true;
            existingMember.UpdatedBy = member.UpdatedBy;
            existingMember.UpdatedDate = member.UpdatedDate;
            await dbContext.SaveChangesAsync();

            return existingMember;
        }

        public async Task<int> GetTotalCount()
        {
            return await dbContext.Members.CountAsync(m => !m.IsRemovedFromView);
        }
    }
}
