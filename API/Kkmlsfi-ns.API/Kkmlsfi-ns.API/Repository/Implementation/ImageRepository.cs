using Kkmlsfi_ns.API.Data;
using Kkmlsfi_ns.API.Models.Domain;
using Kkmlsfi_ns.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Kkmlsfi_ns.API.Repository.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<MemberDisplayPicture> UploadImage(IFormFile file, MemberDisplayPicture memberDisplayPicture)
        {
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{memberDisplayPicture.FileName}{memberDisplayPicture.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var httpRequest = httpContextAccessor.HttpContext.Request;

            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{memberDisplayPicture.FileName}{memberDisplayPicture.FileExtension}";
            memberDisplayPicture.Url = urlPath;

            await dbContext.MemberDisplayPictures.AddAsync(memberDisplayPicture);
            await dbContext.SaveChangesAsync();

            return memberDisplayPicture;
        }

        public async Task<MemberDisplayPicture?> GetMemberDisplayPictureByMemberId(int memberId)
        {
            var memberDisplayPicture = await dbContext.MemberDisplayPictures.FirstOrDefaultAsync(m => m.MemberId == memberId && !m.IsRemovedFromView);

            if (memberDisplayPicture == null)
            {
                return null;
            }

            return memberDisplayPicture;
        }

        public async Task<MemberDisplayPicture?> UpdateMemberDisplayPicture(MemberDisplayPicture memberDisplayPicture)
        {
            if (memberDisplayPicture != null)
            {
                dbContext.MemberDisplayPictures.Update(memberDisplayPicture);
                await dbContext.SaveChangesAsync();

                return memberDisplayPicture;
            }

            return null;
        }
    }
}
