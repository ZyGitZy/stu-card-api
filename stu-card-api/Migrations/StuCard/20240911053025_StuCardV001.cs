using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stu_card_api.Migrations.StuCard
{
    public partial class StuCardV001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StuCard.File",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    BuckName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuCard.File", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StuCard.Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ZhSchoolName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    EnSchoolName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    SchoolAbbreviation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuCard.Template", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StuCard.TemplateItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Version = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuCard.TemplateItem", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StuCard.TemplateItemFabric",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TemplateEntityItemId = table.Column<int>(type: "int", nullable: false),
                    CropX = table.Column<double>(type: "double", nullable: false),
                    CropY = table.Column<double>(type: "double", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    OriginX = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    OriginY = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Left = table.Column<double>(type: "double", nullable: false),
                    Top = table.Column<double>(type: "double", nullable: false),
                    Width = table.Column<double>(type: "double", nullable: false),
                    Height = table.Column<double>(type: "double", nullable: false),
                    Fill = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Stroke = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StrokeWidth = table.Column<double>(type: "double", nullable: false),
                    StrokeDashArray = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StrokeLineCap = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    StrokeDashOffset = table.Column<double>(type: "double", nullable: false),
                    StrokeLineJoin = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    StrokeUniform = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StrokeMiterLimit = table.Column<double>(type: "double", nullable: false),
                    ScaleX = table.Column<double>(type: "double", nullable: false),
                    ScaleY = table.Column<double>(type: "double", nullable: false),
                    Angle = table.Column<double>(type: "double", nullable: false),
                    FlipX = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FlipY = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Opacity = table.Column<double>(type: "double", nullable: false),
                    Shadow = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BackgroundColor = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    FillRule = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    PaintFirst = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    GlobalCompositeOperation = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    SkewX = table.Column<double>(type: "double", nullable: false),
                    SkewY = table.Column<double>(type: "double", nullable: false),
                    Src = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    CrossOrigin = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Filters = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FontSize = table.Column<double>(type: "double", nullable: false),
                    FontWeight = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    FontFamily = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    FontStyle = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    LineHeight = table.Column<double>(type: "double", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    CharSpacing = table.Column<double>(type: "double", nullable: false),
                    TextAlign = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Styles = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PathStartOffset = table.Column<double>(type: "double", nullable: false),
                    PathSide = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    PathAlign = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Underline = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Overline = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Linethrough = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TextBackgroundColor = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuCard.TemplateItemFabric", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StuCard.TemplateItemShadow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TemplateEntityItemId = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Blur = table.Column<double>(type: "double", nullable: false),
                    OffsetX = table.Column<double>(type: "double", nullable: false),
                    OffsetY = table.Column<double>(type: "double", nullable: false),
                    AffectStroke = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    NonScaling = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuCard.TemplateItemShadow", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StuCard.File");

            migrationBuilder.DropTable(
                name: "StuCard.Template");

            migrationBuilder.DropTable(
                name: "StuCard.TemplateItem");

            migrationBuilder.DropTable(
                name: "StuCard.TemplateItemFabric");

            migrationBuilder.DropTable(
                name: "StuCard.TemplateItemShadow");
        }
    }
}
