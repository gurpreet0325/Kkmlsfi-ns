using Kkmlsfi_ns.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Kkmlsfi_ns.API.Models.Domain
{
    public class Member
    {
        public int MemberId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string City { get; set; } = null!;

        public string InsertedBy { get; set; } = null!;

        public DateTime InsertedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsRemovedFromView { get; set; }

        public virtual ICollection<MembersAttendance> MembersAttendances { get; set; } = new List<MembersAttendance>();
    }
}
