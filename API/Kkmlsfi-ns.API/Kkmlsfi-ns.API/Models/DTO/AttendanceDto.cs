namespace Kkmlsfi_ns.API.Models.DTO
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public bool IsFinalized { get; set; }

        public int AttendedMembersCount { get; set; }
    }
}
