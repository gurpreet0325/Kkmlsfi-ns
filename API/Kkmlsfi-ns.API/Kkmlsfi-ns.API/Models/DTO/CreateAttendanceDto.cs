namespace Kkmlsfi_ns.API.Models.DTO
{
    public class CreateAttendanceDto
    {
        public DateTime AttendanceDate { get; set; }

        public string UserEmail { get; set; } = null!;

        public DateTime ActionDateTime { get; set; }
    }
}
