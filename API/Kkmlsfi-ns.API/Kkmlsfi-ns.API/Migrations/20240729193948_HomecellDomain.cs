using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kkmlsfi_ns.API.Migrations
{
    /// <inheritdoc />
    public partial class HomecellDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Homecells",
                columns: table => new
                {
                    HomecellId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomecellDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningPrayerMemberId = table.Column<int>(type: "int", nullable: false),
                    PlaceMemberId = table.Column<int>(type: "int", nullable: false),
                    TeacherMemberId = table.Column<int>(type: "int", nullable: false),
                    InsertedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemovedFromView = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homecells", x => x.HomecellId);
                    table.ForeignKey(
                        name: "FK_Homecells_Members_OpeningPrayerMemberId",
                        column: x => x.OpeningPrayerMemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Homecells_Members_PlaceMemberId",
                        column: x => x.PlaceMemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Homecells_Members_TeacherMemberId",
                        column: x => x.TeacherMemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HomecellPraiseAndWorshipMembers",
                columns: table => new
                {
                    HomecellPraiseAndWorshipMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomecellId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    InsertedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemovedFromView = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomecellPraiseAndWorshipMembers", x => x.HomecellPraiseAndWorshipMemberId);
                    table.ForeignKey(
                        name: "FK_HomecellPraiseAndWorshipMembers_Homecells_HomecellId",
                        column: x => x.HomecellId,
                        principalTable: "Homecells",
                        principalColumn: "HomecellId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomecellPraiseAndWorshipMembers_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomecellPraiseAndWorshipMembers_HomecellId",
                table: "HomecellPraiseAndWorshipMembers",
                column: "HomecellId");

            migrationBuilder.CreateIndex(
                name: "IX_HomecellPraiseAndWorshipMembers_MemberId",
                table: "HomecellPraiseAndWorshipMembers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Homecells_OpeningPrayerMemberId",
                table: "Homecells",
                column: "OpeningPrayerMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Homecells_PlaceMemberId",
                table: "Homecells",
                column: "PlaceMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Homecells_TeacherMemberId",
                table: "Homecells",
                column: "TeacherMemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomecellPraiseAndWorshipMembers");

            migrationBuilder.DropTable(
                name: "Homecells");
        }
    }
}
