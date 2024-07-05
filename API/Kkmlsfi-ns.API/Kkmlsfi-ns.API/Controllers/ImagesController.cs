using AutoMapper;
using Kkmlsfi_ns.API.Models.Domain;
using Kkmlsfi_ns.API.Models.DTO;
using Kkmlsfi_ns.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kkmlsfi_ns.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly IMemberRepository memberRepository;
        private readonly IMapper mapper;

        public ImagesController(IImageRepository imageRepository, IMemberRepository memberRepository, IMapper mapper)
        {
            this.imageRepository = imageRepository;
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }

        //get: /api/images/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMemberDisplayPictureByMemberId([FromRoute] int id)
        {
            var memberDisplayPicture = await imageRepository.GetMemberDisplayPictureByMemberId(id);

            if (memberDisplayPicture == null)
            {
                return NotFound();
            }

            var response = mapper.Map<MemberDisplayPictureDto>(memberDisplayPicture);
            return Ok(response);
        }

        //post: /api/images
        [HttpPost]
        public async Task<IActionResult> UploadDisplayPicture([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] int memberId)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                var member = await memberRepository.GetMemberByIdAsync(memberId);

                if (member != null)
                {
                    var existingMemberDisplayPicture = await imageRepository.GetMemberDisplayPictureByMemberId(memberId);

                    if (existingMemberDisplayPicture != null)
                    {
                        existingMemberDisplayPicture.IsRemovedFromView = true;
                        existingMemberDisplayPicture.UpdatedBy = "gurpreet.pabla";
                        existingMemberDisplayPicture.UpdatedDate = DateTime.Now;

                        await imageRepository.UpdateMemberDisplayPicture(existingMemberDisplayPicture);
                    }

                    var memberDisplayPicture = new MemberDisplayPicture
                    {
                        FileName = fileName,
                        FileExtension = Path.GetExtension(file.FileName).ToLower(),
                        MemberId = memberId,
                        InsertedBy = "gurpreet.pabla",
                        InsertedDate = DateTime.Now,
                        IsRemovedFromView = false,
                        Member = member
                    };

                    memberDisplayPicture = await imageRepository.UploadImage(file, memberDisplayPicture);

                    var response = mapper.Map<MemberDisplayPictureDto>(memberDisplayPicture);

                    return Ok(response);
                }

                return BadRequest();
            }

            return BadRequest();
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format.");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB.");
            }
        }
    }
}
