namespace Kkmlsfi_ns.API.Models.Domain
{
    public class MembersAttendance
    {
        public int MembersAttendanceId { get; set; }

        public int AttendanceId { get; set; }

        public int MemberId { get; set; }

        public double Tithe { get; set; }

        public double Offering { get; set; }

        public double Mission { get; set; }

        public double LoveGift { get; set; }

        public double BuildingFund { get; set; }

        public double Others { get; set; }

        public string? Note { get; set; }

        public string InsertedBy { get; set; } = null!;

        public DateTime InsertedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsRemovedFromView { get; set; }

        public virtual Attendance Attendance { get; set; } = null!;

        public virtual Member Member { get; set; } = null!;
    }
}
