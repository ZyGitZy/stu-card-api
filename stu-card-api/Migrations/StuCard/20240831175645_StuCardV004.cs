using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stu_card_api.Migrations.StuCard
{
    public partial class StuCardV004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "StuCard.TemplateItemShadow",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "StuCard.TemplateItemShadow",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "StuCard.TemplateItemFabric",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "StuCard.TemplateItemFabric",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "StuCard.TemplateItem",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "StuCard.TemplateItem",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "StuCard.Template",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "StuCard.Template",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "StuCard.File",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "StuCard.File",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "StuCard.TemplateItemShadow");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "StuCard.TemplateItemShadow");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "StuCard.TemplateItemFabric");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "StuCard.TemplateItemFabric");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "StuCard.TemplateItem");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "StuCard.TemplateItem");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "StuCard.Template");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "StuCard.Template");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "StuCard.File");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "StuCard.File");
        }
    }
}
