namespace Kkmlsfi_ns.API.Models.DTO
{
    public class UpdateAttendanceDto
    {
        public int AttendanceId { get; set; }

        public string UserEmail { get; set; } = null!;

        public DateTime ActionDateTime { get; set; }

        public bool IsRemovedFromView { get; set; }

        public bool IsFinal { get; set; }
    }
}
