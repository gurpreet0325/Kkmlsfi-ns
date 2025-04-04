namespace Kkmlsfi_ns.API.Models.Domain
{
    public partial class Homecell
    {
        public int HomecellId { get; set; }

        public DateTime HomecellDate { get; set; }

        public string City { get; set; } = null!;

        public int OpeningPrayerMemberId { get; set; }

        public int PlaceMemberId { get; set; }

        public int TeacherMemberId { get; set; }

        public string InsertedBy { get; set; } = null!;

        public DateTime InsertedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsRemovedFromView { get; set; }

        public Member OpeningPrayerMember { get; set; } = null!;

        public Member PlaceMember { get; set; } = null!;

        public Member TeacherMember { get; set; } = null!;

        public virtual ICollection<HomecellPraiseAndWorshipMember> HomecellPraiseAndWorshipMembers { get; set; } = new List<HomecellPraiseAndWorshipMember>();
    }
}
