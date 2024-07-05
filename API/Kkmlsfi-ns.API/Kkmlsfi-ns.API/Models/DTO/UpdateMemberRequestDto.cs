namespace Kkmlsfi_ns.API.Models.DTO
{
    public class UpdateMemberRequestDto
    {
        public int MemberId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string City { get; set; } = null!;
    }
}
