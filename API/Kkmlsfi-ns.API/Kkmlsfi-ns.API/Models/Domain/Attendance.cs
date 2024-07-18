namespace Kkmlsfi_ns.API.Models.Domain
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public string Description { get; set; } = null!;

        public string InsertedBy { get; set; } = null!;

        public DateTime InsertedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? FinalizedBy { get; set; }

        public DateTime? FinalizedDate { get; set; }

        public bool IsRemovedFromView { get; set; }

        public virtual ICollection<MembersAttendance> MembersAttendances { get; set; } = new List<MembersAttendance>();
    }
}
