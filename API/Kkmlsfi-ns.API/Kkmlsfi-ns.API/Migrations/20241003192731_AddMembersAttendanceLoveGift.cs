using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kkmlsfi_ns.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMembersAttendanceLoveGift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LoveGift",
                table: "MembersAttendances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoveGift",
                table: "MembersAttendances");
        }
    }
}
