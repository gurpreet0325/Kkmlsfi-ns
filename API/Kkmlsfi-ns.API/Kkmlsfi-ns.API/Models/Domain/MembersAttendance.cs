namespace Kkmlsfi_ns.API.Models.Domain
{
    public class MembersAttendance
    {
        public int MembersAttendanceId { get; set; }

        public int AttendanceId { get; set; }

        public int MemberId { get; set; }

        public string InsertedBy { get; set; } = null!;

        public DateTime InsertedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsRemovedFromView { get; set; }

        public virtual Attendance Attendance { get; set; } = null!;

        public virtual Member Member { get; set; } = null!;
    }
}
