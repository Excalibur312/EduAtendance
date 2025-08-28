using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduAtendance.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class mg5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuTemplateSubmenu",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuTemplateSubmenu", x => x.Title);
                    table.ForeignKey(
                        name: "FK_MenuTemplateSubmenu_MenuTemplateSubmenu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MenuTemplateSubmenu",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuTemplateSubmenu_MenuTemplates_MenuTemplateId",
                        column: x => x.MenuTemplateId,
                        principalTable: "MenuTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuTemplateSubmenuItems",
                columns: table => new
                {
                    SubmenuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuTemplateSubmenuItems", x => new { x.SubmenuId, x.Id });
                    table.ForeignKey(
                        name: "FK_MenuTemplateSubmenuItems_MenuTemplateSubmenu_SubmenuId",
                        column: x => x.SubmenuId,
                        principalTable: "MenuTemplateSubmenu",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuTemplateSubmenu_MenuTemplateId",
                table: "MenuTemplateSubmenu",
                column: "MenuTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuTemplateSubmenu_ParentId",
                table: "MenuTemplateSubmenu",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuTemplateSubmenuItems");

            migrationBuilder.DropTable(
                name: "MenuTemplateSubmenu");

            migrationBuilder.DropTable(
                name: "MenuTemplates");
        }
    }
}
