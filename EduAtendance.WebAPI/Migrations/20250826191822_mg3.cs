using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduAtendance.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class mg3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyTemplateCategory",
                columns: table => new
                {
                    SurveyTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTemplateCategory", x => new { x.SurveyTemplateId, x.Id });
                    table.ForeignKey(
                        name: "FK_SurveyTemplateCategory_SurveyTemplates_SurveyTemplateId",
                        column: x => x.SurveyTemplateId,
                        principalTable: "SurveyTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyTemplateCategoryOption",
                columns: table => new
                {
                    SurveyTemplateCategorySurveyTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyTemplateCategoryId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTemplateCategoryOption", x => new { x.SurveyTemplateCategorySurveyTemplateId, x.SurveyTemplateCategoryId, x.Id });
                    table.ForeignKey(
                        name: "FK_SurveyTemplateCategoryOption_SurveyTemplateCategory_SurveyTemplateCategorySurveyTemplateId_SurveyTemplateCategoryId",
                        columns: x => new { x.SurveyTemplateCategorySurveyTemplateId, x.SurveyTemplateCategoryId },
                        principalTable: "SurveyTemplateCategory",
                        principalColumns: new[] { "SurveyTemplateId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyTemplateCategoryQuestion",
                columns: table => new
                {
                    SurveyTemplateCategorySurveyTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyTemplateCategoryId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTemplateCategoryQuestion", x => new { x.SurveyTemplateCategorySurveyTemplateId, x.SurveyTemplateCategoryId, x.Id });
                    table.ForeignKey(
                        name: "FK_SurveyTemplateCategoryQuestion_SurveyTemplateCategory_SurveyTemplateCategorySurveyTemplateId_SurveyTemplateCategoryId",
                        columns: x => new { x.SurveyTemplateCategorySurveyTemplateId, x.SurveyTemplateCategoryId },
                        principalTable: "SurveyTemplateCategory",
                        principalColumns: new[] { "SurveyTemplateId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyTemplateCategoryOption");

            migrationBuilder.DropTable(
                name: "SurveyTemplateCategoryQuestion");

            migrationBuilder.DropTable(
                name: "SurveyTemplateCategory");

            migrationBuilder.DropTable(
                name: "SurveyTemplates");
        }
    }
}
