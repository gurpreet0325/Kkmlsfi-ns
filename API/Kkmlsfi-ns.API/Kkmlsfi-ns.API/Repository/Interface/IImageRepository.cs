using Kkmlsfi_ns.API.Models.Domain;

namespace Kkmlsfi_ns.API.Repository.Interface
{
    public interface IImageRepository
    {
        Task<MemberDisplayPicture> UploadImage(IFormFile file, MemberDisplayPicture memberDisplayPicture);
        Task<MemberDisplayPicture?> GetMemberDisplayPictureByMemberId(int memberId);
        Task<MemberDisplayPicture?> UpdateMemberDisplayPicture(MemberDisplayPicture memberDisplayPicture);
    }
}
