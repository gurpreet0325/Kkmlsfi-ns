namespace Kkmlsfi_ns.API.Models.DTO
{
    public class MemberDisplayPictureDto
    {
        public int MemberDisplayPictureID { get; set; }
        public int MemberId { get; set; }
        public string FileName { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
