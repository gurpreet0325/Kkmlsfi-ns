namespace Kkmlsfi_ns.API.Models.Domain
{
    public class HomecellPraiseAndWorshipMember
    {
        public int HomecellPraiseAndWorshipMemberId { get; set; }

        public int HomecellId { get; set; }

        public int MemberId { get; set; }

        public string InsertedBy { get; set; } = null!;

        public DateTime InsertedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsRemovedFromView { get; set; }

        public Homecell Homecell { get; set; } = null!;

        public Member Member { get; set; } = null!;
    }
}
