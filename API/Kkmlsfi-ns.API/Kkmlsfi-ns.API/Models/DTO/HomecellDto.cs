using Kkmlsfi_ns.API.Models.Domain;

namespace Kkmlsfi_ns.API.Models.DTO
{
    public class HomecellDto
    {
        public int HomecellId { get; set; }

        public DateTime HomecellDate { get; set; }

        public string City { get; set; } = null!;

        public string OpeningPrayerMemberName { get; set; } = null!;

        public string PlaceMemberName { get; set; } = null!;

        public string TeacherMemberName { get; set; } = null!;

        public string PraiseAndWorshipMembers { get; set; } = null!;

        public int OpeningPrayerMemberId { get; set; }

        public int PlaceMemberId { get; set; }

        public int TeacherMemberId { get; set; }

        public IDictionary<int, string> HomecellPraiseAndWorshipMembers { get; set; } = null!;
    }
}
