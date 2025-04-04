using Kkmlsfi_ns.API.Models.Domain;

namespace Kkmlsfi_ns.API.Models.DTO
{
    public class CreateHomecellRequestDto
    {
        public DateTime HomecellDate { get; set; }

        public string City { get; set; } = null!;

        public int OpeningPrayerMemberId { get; set; }

        public int PlaceMemberId { get; set; }

        public int TeacherMemberId { get; set; }

        public IDictionary<int, string> HomecellPraiseAndWorshipMembers { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public DateTime ActionDateTime { get; set; }
    }
}
