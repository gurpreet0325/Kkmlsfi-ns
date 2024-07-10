using AutoMapper;
using Kkmlsfi_ns.API.Data;
using Kkmlsfi_ns.API.Models.Domain;
using Kkmlsfi_ns.API.Models.DTO;
using Kkmlsfi_ns.API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace Kkmlsfi_ns.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository memberRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public MembersController(IMemberRepository memberRepository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        //get api/members/GetAllMembers?searchFilter=name&sortBy=name&sortDirection=desc
        [HttpGet]
        [Route("GetAllMembers")]
        public async Task<IActionResult> GetAllMembers([FromQuery] string? searchFilter, 
                                                       [FromQuery] string? sortBy, 
                                                       [FromQuery] string? sortDirection,
                                                       [FromQuery] int? pageNumber,
                                                       [FromQuery] int? pageSize)
        {
            var members = await memberRepository.GetAllMembersAsync(searchFilter, sortBy, sortDirection, pageNumber, pageSize);
            var response = members.Select(m=> mapper.Map<MemberDto>(m));

            return Ok(response);
        }

        //get api/members/GetMemberById/{id}
        [HttpGet]
        [Route("GetMemberById/{id:int}")]
        public async Task<IActionResult> GetMemberById([FromRoute] int id)
        {
            var member = await memberRepository.GetMemberByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            var response = mapper.Map<MemberDto>(member);

            return Ok(response);
        }

        //post api/members/CreateMember
        [HttpPost]
        [Route("CreateMember")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateMember(CreateMemberRequestDto request)
        {
            var member = mapper.Map<Member>(request);

            member.InsertedBy = request.UserEmail;
            member.InsertedDate = request.ActionDateTime;
            member.IsRemovedFromView = false;

            await memberRepository.CreateAsync(member);

            var response = mapper.Map<MemberDto>(member);

            return Ok(response);
        }

        //put api/members/UpdateMember
        [HttpPut]
        [Route("UpdateMember")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateMember(UpdateMemberRequestDto request)
        {
            var member = mapper.Map<Member>(request);

            member.UpdatedBy = request.UserEmail;
            member.UpdatedDate = request.ActionDateTime;

            member = await memberRepository.UpdateAsync(member);

            if (member == null)
            {
                return NotFound();
            }

            var response = mapper.Map<MemberDto>(member);

            return Ok(response);
        }
        //delete api/Members/DeleteMember
        [HttpPut]
        [Route("DeleteMember")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteMember(UpdateMemberRequestDto request)
        {
            var member = mapper.Map<Member>(request);

            member.UpdatedBy = request.UserEmail;
            member.UpdatedDate = request.ActionDateTime;

            member = await memberRepository.DeleteAsync(member);

            if (member == null)
            {
                return NotFound();
            }

            var response = mapper.Map<MemberDto>(member);

            return Ok(response);
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetMembersTotalCount()
        {
            var totalCount = await memberRepository.GetTotalCount();
            return Ok(totalCount);
        }
    }
}
