using AutoMapper;
using AutoMapper.Execution;
using Azure.Core;
using Kkmlsfi_ns.API.Models.Domain;
using Kkmlsfi_ns.API.Models.DTO;
using Kkmlsfi_ns.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kkmlsfi_ns.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository attendanceRepository;
        private readonly IMemberRepository memberRepository;
        private readonly IMapper mapper;

        public AttendanceController(IAttendanceRepository attendanceRepository,IMemberRepository memberRepository, IMapper mapper)
        {
            this.attendanceRepository = attendanceRepository;
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetMembersAttendanceById/{id:int}")]
        public async Task<IActionResult> GetMembersAttendanceById([FromRoute] int id)
        {
            var attendance = await attendanceRepository.GetAttendanceByIdAsync(id);

            if (attendance != null)
            {
                var membersAttendances = await attendanceRepository.GetMembersAttendancesByIdAsync(id);

                var response = from m in membersAttendances
                               select new MembersAttendanceDto
                               {
                                   MembersAttendanceId = m.MembersAttendanceId,
                                   AttendanceId = m.AttendanceId,
                                   MemberId = m.Member.MemberId,
                                   FullName = string.IsNullOrEmpty(m.Member.MiddleName) ? $"{m.Member.LastName}, {m.Member.FirstName}" : $"{m.Member.LastName}, {m.Member.FirstName} {m.Member.MiddleName}"
                               };

                return Ok(response.OrderBy(a => a.FullName));
            }

            return NotFound();
        }

        [HttpGet]
        [Route("GetAttendanceById/{id:int}")]
        public async Task<IActionResult> GetAttendanceById([FromRoute] int id)
        {
            var attendance = await attendanceRepository.GetAttendanceByIdAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            var response = new AttendanceDto
                           {
                               AttendanceId = attendance.AttendanceId,
                               AttendanceDate = attendance.AttendanceDate,
                               IsFinalized = attendance.FinalizedDate != null,
                               AttendedMembersCount = attendance.MembersAttendances.Count(m => m.HasAttended && !m.IsRemovedFromView)
                           };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllAttendaces")]
        public async Task<IActionResult> GetAllAttendances([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var attendances = await attendanceRepository.GetAllAttendancesAsync(pageNumber, pageSize);

            var response = from att in attendances
                           select new AttendanceDto
                           {
                               AttendanceId = att.AttendanceId,
                               AttendanceDate = att.AttendanceDate,
                               IsFinalized = att.FinalizedDate != null,
                               AttendedMembersCount = att.MembersAttendances.Count(m => m.HasAttended && !m.IsRemovedFromView)
                           };

            return Ok(response);
        }

        [HttpPost]
        [Route("CreateAttendance")]
        public async Task<IActionResult> CreateAttendance(CreateAttendanceDto request)
        {
            var attendance = new Attendance
            {
                AttendanceDate = request.AttendanceDate,
                Description = "Sunday Service",
                InsertedBy = request.UserEmail,
                InsertedDate = request.ActionDateTime,
                IsRemovedFromView = false
            };

            await attendanceRepository.CreateAsync(attendance);

            var response = new AttendanceDto
                           {
                               AttendanceId = attendance.AttendanceId,
                               AttendanceDate = attendance.AttendanceDate,
                               IsFinalized = attendance.FinalizedDate != null,
                               AttendedMembersCount = 0
                           };

            return Ok(response);
        }

        [HttpPost]
        [Route("CreateMembersAttendance")]
        public async Task<IActionResult> CreateMembersAttendance(CreateMemberAttendanceDto request)
        {
            var membersAttendance = new MembersAttendance
            {
                AttendanceId = request.AttendanceId,
                MemberId = request.MemberId,
                InsertedBy = request.UserEmail,
                InsertedDate = request.ActionDateTime,
                IsRemovedFromView = false,
                HasAttended = true
            };

            await attendanceRepository.CreateMembersAttendanceAsync(membersAttendance);

            var response = mapper.Map<MembersAttendanceDto>(membersAttendance);

            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateAttendance")]
        public async Task<IActionResult> UpdateAttendance(UpdateAttendanceDto request)
        {
            var attendance = await attendanceRepository.GetAttendanceByIdAsync(request.AttendanceId);
            if (attendance != null)
            {
                if (request.IsFinal)
                {
                    attendance.FinalizedBy = request.UserEmail;
                    attendance.FinalizedDate = request.ActionDateTime;
                }

                if (request.IsRemovedFromView)
                {
                    attendance.IsRemovedFromView = true;
                }

                attendance.UpdatedBy = request.UserEmail;
                attendance.UpdatedDate = request.ActionDateTime;

                attendance = await attendanceRepository.UpdateAsync(attendance);

                var response = new AttendanceDto
                {
                    AttendanceId = attendance.AttendanceId,
                    AttendanceDate = attendance.AttendanceDate,
                    IsFinalized = attendance.FinalizedDate != null,
                    AttendedMembersCount = attendance.MembersAttendances.Count()
                };

                return Ok(response);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("DeleteMembersAttendanceById/{id:int}")]
        public async Task<IActionResult> DeleteMembersAttendanceById(int id)
        {
            var membersAttendance = await attendanceRepository.GetMembersAttendanceByIdAsync(id);
            if (membersAttendance != null)
            {
                await attendanceRepository.DeleteMembersAttendanceAsync(membersAttendance);

                var response = mapper.Map<MembersAttendanceDto>(membersAttendance);
                return Ok(response);
            }

            return NotFound();
        }
    }
}
