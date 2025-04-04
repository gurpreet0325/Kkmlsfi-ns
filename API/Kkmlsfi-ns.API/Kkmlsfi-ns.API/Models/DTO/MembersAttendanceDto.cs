namespace Kkmlsfi_ns.API.Models.DTO
{
    public class MembersAttendanceDto
    {
        public int MembersAttendanceId { get; set; }

        public int AttendanceId { get; set; }

        public int MemberId { get; set; }

        public string FullName { get; set; } = null!;

        public double Tithe { get; set; }

        public double Offering { get; set; }

        public double Mission { get; set; }

        public double LoveGift { get; set; }

        public double BuildingFund { get; set; }

        public double Others { get; set; }

        public string? Note { get; set; }
    }
}
