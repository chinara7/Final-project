using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class CreateDeactivationColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Teachers",
                type: "datetime2",
                defaultValue: DateTime.UtcNow.AddHours(4),
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationTime",
                table: "Teachers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "Teachers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Events",
                type: "datetime2",
                defaultValue: DateTime.UtcNow.AddHours(4),
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationTime",
                table: "Events",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Blogs",
                type: "datetime2",
                defaultValue: DateTime.UtcNow.AddHours(4),
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationTime",
                table: "Blogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "DeactivationTime",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DeactivationTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "DeactivationTime",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "Blogs");
        }
    }
}
