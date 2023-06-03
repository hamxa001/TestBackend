using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestBackend.Migrations
{
    public partial class LeaveTableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "leaves",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "leaves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "leaves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "leaves",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaves");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "leaves");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "leaves");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "leaves");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "leaves");
        }
    }
}
