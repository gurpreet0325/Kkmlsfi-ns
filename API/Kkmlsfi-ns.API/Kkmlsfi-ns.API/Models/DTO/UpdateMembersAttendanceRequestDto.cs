using static Kkmlsfi_ns.API.Models.Enums.Enums;

namespace Kkmlsfi_ns.API.Models.DTO
{
    public class UpdateMembersAttendanceRequestDto
    {
        public int MembersAttendanceId { get; set; }
        public ValueTypes ValueType { get; set; }
        public double Value { get; set; }
        public string? Note { get; set; }
        public string UserEmail { get; set; } = null!;
        public DateTime ActionDateTime { get; set; }
    }
}
