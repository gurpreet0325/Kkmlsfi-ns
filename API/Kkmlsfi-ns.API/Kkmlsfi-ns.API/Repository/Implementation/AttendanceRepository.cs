using AutoMapper.Execution;
using Kkmlsfi_ns.API.Data;
using Kkmlsfi_ns.API.Models.Domain;
using Kkmlsfi_ns.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Kkmlsfi_ns.API.Repository.Implementation
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AttendanceRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Attendance>> GetAllAttendancesAsync(int? pageNumber = 1, int? pageSize = 100)
        {
            var attendances = dbContext.Attendances
                .Where(a => !a.IsRemovedFromView)
                .Include(a => a.MembersAttendances)
                .OrderByDescending(a=>a.AttendanceDate)
                .AsQueryable();

            var skipResults = (pageNumber - 1) * pageSize;
            attendances = attendances.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await attendances.ToListAsync();
        }

        public async Task<Attendance?> GetAttendanceByIdAsync(int attendanceId)
        {
            return await dbContext.Attendances.Include(a => a.MembersAttendances).FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);
        }

        public async Task<IEnumerable<MembersAttendance>> GetMembersAttendancesByIdAsync(int attendanceId)
        {
            var membersAttendance = dbContext.MembersAttendances
                .Where(ma => ma.AttendanceId == attendanceId && ma.HasAttended)
                .Include(ma => ma.Member);

            return await membersAttendance.ToListAsync();
        }

        public async Task<Attendance> CreateAsync(Attendance attendance)
        {
            await dbContext.Attendances.AddAsync(attendance);
            await dbContext.SaveChangesAsync();
            return attendance;
        }

        public async Task<MembersAttendance> CreateMembersAttendanceAsync(MembersAttendance membersAttendance)
        {
            await dbContext.MembersAttendances.AddAsync(membersAttendance);
            await dbContext.SaveChangesAsync();
            return membersAttendance;
        }

        public async Task<Attendance> UpdateAsync(Attendance attendance)
        {
            dbContext.Attendances.Update(attendance);
            await dbContext.SaveChangesAsync();
            return attendance;
        }

        public async Task<MembersAttendance?> DeleteMembersAttendanceAsync(MembersAttendance membersAttendance)
        {
            dbContext.MembersAttendances.Remove(membersAttendance);
            await dbContext.SaveChangesAsync();
            return membersAttendance;
        }

        public async Task<MembersAttendance?> GetMembersAttendanceByIdAsync(int membersAttendanceId)
        {
            var membersAttendance = await dbContext.MembersAttendances
                .FirstOrDefaultAsync(ma => ma.MembersAttendanceId == membersAttendanceId);

            return membersAttendance;
        }
    }
}
