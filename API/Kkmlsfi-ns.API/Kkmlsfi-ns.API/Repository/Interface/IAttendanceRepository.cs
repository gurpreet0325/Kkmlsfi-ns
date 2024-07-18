using Kkmlsfi_ns.API.Models.Domain;

namespace Kkmlsfi_ns.API.Repository.Interface
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAttendancesAsync(int? pageNumber = 1, int? pageSize = 100);
        Task<Attendance?> GetAttendanceByIdAsync(int attendanceId);
        Task<IEnumerable<MembersAttendance>> GetMembersAttendancesByIdAsync(int attendanceId);
        Task<Attendance> CreateAsync(Attendance attendance);
        Task<MembersAttendance> CreateMembersAttendanceAsync(MembersAttendance membersAttendance);
        Task<Attendance> UpdateAsync(Attendance attendance);
        Task<MembersAttendance?> DeleteMembersAttendanceAsync(MembersAttendance membersAttendance);
        Task<MembersAttendance?> GetMembersAttendanceByIdAsync(int membersAttendanceId);
    }
}
