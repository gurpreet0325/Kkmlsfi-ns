namespace Kkmlsfi_ns.API.Models.DTO
{
    public class MembersAttendanceDto
    {
        public int MembersAttendanceId { get; set; }

        public int AttendanceId { get; set; }

        public int MemberId { get; set; }

        public string FullName { get; set; } = null!;
    }
}
