using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kkmlsfi_ns.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendanceFinalize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinalizedBy",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalizedDate",
                table: "Attendances",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalizedBy",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "FinalizedDate",
                table: "Attendances");
        }
    }
}
