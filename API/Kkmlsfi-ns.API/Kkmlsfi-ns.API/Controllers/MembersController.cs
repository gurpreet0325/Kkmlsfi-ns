using AutoMapper;
using Kkmlsfi_ns.API.Data;
using Kkmlsfi_ns.API.Models.Domain;
using Kkmlsfi_ns.API.Models.DTO;
using Kkmlsfi_ns.API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public MembersController(IMemberRepository memberRepository, IMapper mapper)
        {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }

        //get api/members
        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await memberRepository.GetAllMembersAsync();
            var response = members.Select(m=> mapper.Map<MemberDto>(m));

            return Ok(response);
        }

        //get api/members/{id}
        [HttpGet]
        [Route("{id:int}")]
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

        //post api/members
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateMember(CreateMemberRequestDto request)
        {
            var member = mapper.Map<Member>(request);

            member.InsertedBy = "gurpreet.pabla";
            member.InsertedDate = DateTime.Now;
            member.IsRemovedFromView = false;

            await memberRepository.CreateAsync(member);

            var response = mapper.Map<MemberDto>(member);

            return Ok(response);
        }

        //put api/members
        [HttpPut]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateMember(UpdateMemberRequestDto request)
        {
            var member = mapper.Map<Member>(request);

            member.UpdatedBy = "gurpreet.pabla";
            member.UpdatedDate = DateTime.Now;

            member = await memberRepository.UpdateAsync(member);

            if (member == null)
            {
                return NotFound();
            }

            var response = mapper.Map<MemberDto>(member);

            return Ok(response);
        }
        //delete api/Members
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteMember([FromRoute] int id)
        {
            var member = new Member
            {
                MemberId = id,
                IsRemovedFromView = true,
                UpdatedBy = "gurpreet.pabla",
                UpdatedDate = DateTime.Now
            };

            member = await memberRepository.DeleteAsync(member);

            if (member == null)
            {
                return NotFound();
            }

            var response = mapper.Map<MemberDto>(member);

            return Ok(response);
        }
    }
}
