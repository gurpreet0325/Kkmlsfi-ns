using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kkmlsfi_ns.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMembersAttendanceTithesAndOfferings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homecells_Members_OpeningPrayerMemberId",
                table: "Homecells");

            migrationBuilder.DropForeignKey(
                name: "FK_Homecells_Members_PlaceMemberId",
                table: "Homecells");

            migrationBuilder.DropForeignKey(
                name: "FK_Homecells_Members_TeacherMemberId",
                table: "Homecells");

            migrationBuilder.DropColumn(
                name: "HasAttended",
                table: "MembersAttendances");

            migrationBuilder.AddColumn<double>(
                name: "BuildingFund",
                table: "MembersAttendances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mission",
                table: "MembersAttendances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "MembersAttendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Offering",
                table: "MembersAttendances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Others",
                table: "MembersAttendances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Tithe",
                table: "MembersAttendances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Homecells_Members_OpeningPrayerMemberId",
                table: "Homecells",
                column: "OpeningPrayerMemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Homecells_Members_PlaceMemberId",
                table: "Homecells",
                column: "PlaceMemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Homecells_Members_TeacherMemberId",
                table: "Homecells",
                column: "TeacherMemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homecells_Members_OpeningPrayerMemberId",
                table: "Homecells");

            migrationBuilder.DropForeignKey(
                name: "FK_Homecells_Members_PlaceMemberId",
                table: "Homecells");

            migrationBuilder.DropForeignKey(
                name: "FK_Homecells_Members_TeacherMemberId",
                table: "Homecells");

            migrationBuilder.DropColumn(
                name: "BuildingFund",
                table: "MembersAttendances");

            migrationBuilder.DropColumn(
                name: "Mission",
                table: "MembersAttendances");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "MembersAttendances");

            migrationBuilder.DropColumn(
                name: "Offering",
                table: "MembersAttendances");

            migrationBuilder.DropColumn(
                name: "Others",
                table: "MembersAttendances");

            migrationBuilder.DropColumn(
                name: "Tithe",
                table: "MembersAttendances");

            migrationBuilder.AddColumn<bool>(
                name: "HasAttended",
                table: "MembersAttendances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Homecells_Members_OpeningPrayerMemberId",
                table: "Homecells",
                column: "OpeningPrayerMemberId",
                principalTable: "Members",
                principalColumn: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homecells_Members_PlaceMemberId",
                table: "Homecells",
                column: "PlaceMemberId",
                principalTable: "Members",
                principalColumn: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homecells_Members_TeacherMemberId",
                table: "Homecells",
                column: "TeacherMemberId",
                principalTable: "Members",
                principalColumn: "MemberId");
        }
    }
}
