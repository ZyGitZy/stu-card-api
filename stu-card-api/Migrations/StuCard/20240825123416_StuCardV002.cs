using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stu_card_api.Migrations.StuCard
{
    public partial class StuCardV002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "StuCard.Template");

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "StuCard.TemplateItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "StuCard.Template",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "StuCard.TemplateItem");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "StuCard.Template");

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "StuCard.Template",
                type: "nvarchar(2000)",
                nullable: false,
                defaultValue: "");
        }
    }
}
