using Kkmlsfi_ns.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Kkmlsfi_ns.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<MembersAttendance> MembersAttendances { get; set; }
        public DbSet<MemberDisplayPicture> MemberDisplayPictures { get; set; }
    }
}
