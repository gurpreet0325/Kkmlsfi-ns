using Kkmlsfi_ns.API.Models.Domain;
using Kkmlsfi_ns.API.Models.DTO;
using Kkmlsfi_ns.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kkmlsfi_ns.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomecellController : ControllerBase
    {
        private readonly IHomecellRepository homecellRepository;
        private readonly IMemberRepository memberRepository;

        public HomecellController(IHomecellRepository homecellRepository, IMemberRepository memberRepository)
        {
            this.homecellRepository = homecellRepository;
            this.memberRepository = memberRepository;
        }

        [HttpGet]
        [Route("GetAllHomecells")]
        public async Task<IActionResult> GetAllHomecells([FromQuery] string? searchFilter,
                                                         [FromQuery] string? sortBy,
                                                         [FromQuery] string? sortDirection,
                                                         [FromQuery] int? pageNumber,
                                                         [FromQuery] int? pageSize)
        {
            var homecells = await homecellRepository.GetAllHomecellsAsync();

            var response = from homecell in homecells
                           select new HomecellDto
                           {
                               HomecellId = homecell.HomecellId,
                               HomecellDate = homecell.HomecellDate,
                               City = homecell.City,
                               OpeningPrayerMemberName = homecell.OpeningPrayerMember.FullName,
                               PlaceMemberName = homecell.PlaceMember.FullName,
                               TeacherMemberName = homecell.TeacherMember.FullName,
                               PraiseAndWorshipMembers = homecell.PraiseAndWorshipMembers
                           };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetHomecellById:{id:int}")]
        public async Task<IActionResult> GetHomecellById(int id)
        {
            var homecell = await homecellRepository.GetHomecellByIdAsync(id);

            if (homecell == null)
            {
                return NotFound();
            }

            var response = new HomecellDto
            {
                HomecellId = homecell.HomecellId,
                HomecellDate = homecell.HomecellDate,
                City = homecell.City,
                OpeningPrayerMemberId = homecell.OpeningPrayerMemberId,
                PlaceMemberId = homecell.PlaceMemberId,
                TeacherMemberId = homecell.TeacherMemberId,
                HomecellPraiseAndWorshipMembers = homecell.HomecellPraiseAndWorshipMembers.ToDictionary(h => h.MemberId, h => h.Member.FullName)
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("CreateHomecell")]
        public async Task<IActionResult> CreateHomecell(CreateHomecellRequestDto request)
        {
            var homecell = new Homecell
            {
                HomecellDate = request.HomecellDate,
                City = request.City,
                OpeningPrayerMemberId = request.OpeningPrayerMemberId,
                PlaceMemberId = request.PlaceMemberId,
                TeacherMemberId = request.TeacherMemberId,
                InsertedBy = request.UserEmail,
                InsertedDate = request.ActionDateTime,
                IsRemovedFromView = false
            };

            foreach (var pnw in request.HomecellPraiseAndWorshipMembers)
            {
                homecell.HomecellPraiseAndWorshipMembers.Add(new HomecellPraiseAndWorshipMember
                {
                    MemberId = pnw.Key,
                    InsertedBy = request.UserEmail,
                    InsertedDate = request.ActionDateTime,
                    IsRemovedFromView = false
                });
            }

            await homecellRepository.CreateAsync(homecell);

            var response = new HomecellDto
            {
                HomecellId = homecell.HomecellId,
                HomecellDate = homecell.HomecellDate,
                City = homecell.City,
                OpeningPrayerMemberId = homecell.OpeningPrayerMemberId,
                PlaceMemberId = homecell.PlaceMemberId,
                TeacherMemberId = homecell.TeacherMemberId,
                HomecellPraiseAndWorshipMembers = request.HomecellPraiseAndWorshipMembers
            };

            return Ok(response);
        }
    }
}
