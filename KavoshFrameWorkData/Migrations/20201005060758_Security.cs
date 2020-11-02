using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KavoshFrameWorkData.Migrations
{
    public partial class Security : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleFormActionAssignment_AspNetRoles_RoleId",
                table: "RoleFormActionAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleFormActionAssignment",
                table: "RoleFormActionAssignment");

            migrationBuilder.RenameTable(
                name: "RoleFormActionAssignment",
                newName: "RoleFormActionAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_RoleFormActionAssignment_RoleId",
                table: "RoleFormActionAssignments",
                newName: "IX_RoleFormActionAssignments_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleFormActionAssignments",
                table: "RoleFormActionAssignments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SystemForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ParentTitle = table.Column<string>(nullable: true),
                    EntityName = table.Column<string>(nullable: true),
                    ListOnly = table.Column<bool>(nullable: false),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemForms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleFormActionAssignments_SystemFormId",
                table: "RoleFormActionAssignments",
                column: "SystemFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleFormActionAssignments_AspNetRoles_RoleId",
                table: "RoleFormActionAssignments",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleFormActionAssignments_SystemForms_SystemFormId",
                table: "RoleFormActionAssignments",
                column: "SystemFormId",
                principalTable: "SystemForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleFormActionAssignments_AspNetRoles_RoleId",
                table: "RoleFormActionAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleFormActionAssignments_SystemForms_SystemFormId",
                table: "RoleFormActionAssignments");

            migrationBuilder.DropTable(
                name: "SystemForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleFormActionAssignments",
                table: "RoleFormActionAssignments");

            migrationBuilder.DropIndex(
                name: "IX_RoleFormActionAssignments_SystemFormId",
                table: "RoleFormActionAssignments");

            migrationBuilder.RenameTable(
                name: "RoleFormActionAssignments",
                newName: "RoleFormActionAssignment");

            migrationBuilder.RenameIndex(
                name: "IX_RoleFormActionAssignments_RoleId",
                table: "RoleFormActionAssignment",
                newName: "IX_RoleFormActionAssignment_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleFormActionAssignment",
                table: "RoleFormActionAssignment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleFormActionAssignment_AspNetRoles_RoleId",
                table: "RoleFormActionAssignment",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
