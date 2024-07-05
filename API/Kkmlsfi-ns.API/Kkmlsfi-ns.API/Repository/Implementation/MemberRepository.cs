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

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await dbContext.Members.Where(m => !m.IsRemovedFromView).ToListAsync();
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

            existingMember.IsRemovedFromView = member.IsRemovedFromView;
            existingMember.UpdatedBy = member.UpdatedBy;
            existingMember.UpdatedDate = member.UpdatedDate;
            await dbContext.SaveChangesAsync();

            return existingMember;
        }
    }
}
