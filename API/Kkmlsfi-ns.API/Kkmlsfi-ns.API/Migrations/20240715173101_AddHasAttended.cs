using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kkmlsfi_ns.API.Migrations
{
    /// <inheritdoc />
    public partial class AddHasAttended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasAttended",
                table: "MembersAttendances",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAttended",
                table: "MembersAttendances");
        }
    }
}
