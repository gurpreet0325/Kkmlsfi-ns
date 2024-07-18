namespace Kkmlsfi_ns.API.Models.DTO
{
    public class CreateMemberAttendanceDto
    {
        public int AttendanceId { get; set; }

        public int MemberId { get; set; }

        public string UserEmail { get; set; } = null!;

        public DateTime ActionDateTime { get; set; }
    }
}
