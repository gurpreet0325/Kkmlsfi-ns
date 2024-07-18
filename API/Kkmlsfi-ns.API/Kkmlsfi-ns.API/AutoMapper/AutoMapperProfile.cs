using AutoMapper;
using Kkmlsfi_ns.API.Models.Domain;
using Kkmlsfi_ns.API.Models.DTO;

namespace Kkmlsfi_ns.API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<CreateMemberRequestDto, Member>();
            CreateMap<UpdateMemberRequestDto, Member>();
            CreateMap<MemberDisplayPicture, MemberDisplayPictureDto>();
            CreateMap<MembersAttendance,MembersAttendanceDto>();
        }
    }
}
