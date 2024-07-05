using System.Security.Policy;

namespace Kkmlsfi_ns.API.Models.Domain
{
    public class MemberDisplayPicture
    {
        public int MemberDisplayPictureID { get; set; }
        public int MemberId { get; set; }
        public string FileName { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string InsertedBy { get; set; } = null!;
        public DateTime InsertedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsRemovedFromView { get; set; }
        public Member Member { get; set; } = new Member();
    }
}
