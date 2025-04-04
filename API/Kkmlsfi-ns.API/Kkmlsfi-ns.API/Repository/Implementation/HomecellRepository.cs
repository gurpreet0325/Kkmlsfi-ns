using Kkmlsfi_ns.API.Data;
using Kkmlsfi_ns.API.Models.Domain;
using Kkmlsfi_ns.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace Kkmlsfi_ns.API.Repository.Implementation
{
    public class HomecellRepository : IHomecellRepository
    {
        private readonly ApplicationDbContext dbContext;

        public HomecellRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Homecell>> GetAllHomecellsAsync(int? pageNumber = 1, int? pageSize = 100)
        {
            var homecells = dbContext.Homecells
                .Where(h => !h.IsRemovedFromView)
                .Include(h => h.OpeningPrayerMember)
                .Include(h => h.PlaceMember)
                .Include(h => h.TeacherMember)
                .Include(h => h.HomecellPraiseAndWorshipMembers).ThenInclude(m => m.Member)
                .OrderByDescending(h => h.HomecellDate)
                .AsQueryable();

            var skipResults = (pageNumber - 1) * pageSize;
            homecells = homecells.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await homecells.ToListAsync();
        }

        public async Task<Homecell?> GetHomecellByIdAsync(int homecellId)
        {
            var homecell = await dbContext.Homecells
                .Where(h=>h.HomecellId == homecellId)
                .Include(h => h.HomecellPraiseAndWorshipMembers).ThenInclude(m => m.Member)
                .FirstOrDefaultAsync();

            return homecell;
        }

        public async Task<Homecell> CreateAsync(Homecell homecell)
        {
            await dbContext.Homecells.AddAsync(homecell);
            dbContext.SaveChanges();
            return homecell;
        }
    }
}
