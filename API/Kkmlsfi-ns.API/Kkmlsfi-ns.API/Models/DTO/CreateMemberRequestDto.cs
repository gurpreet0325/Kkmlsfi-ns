namespace Kkmlsfi_ns.API.Models.DTO
{
    public class CreateMemberRequestDto
    {
        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string City { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public DateTime ActionDateTime { get; set; }
    }
}
