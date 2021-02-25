using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xyz.lsyyy.Verification.Data.Migrations
{
    public partial class Add_TagName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentActionMaps_Actions_ActionId",
                table: "DepartmentActionMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionActionMaps_Actions_ActionId",
                table: "PositionActionMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActionMaps_Actions_ActionId",
                table: "UserActionMaps");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.AddColumn<Guid>(
                name: "ActionTagId",
                table: "UserActionMaps",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActionTagId",
                table: "PositionActionMaps",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActionTagId",
                table: "DepartmentActionMaps",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActionTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ControllerName = table.Column<string>(nullable: true),
                    ActionName = table.Column<string>(nullable: true),
                    TagName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserActionMaps_ActionTagId",
                table: "UserActionMaps",
                column: "ActionTagId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionActionMaps_ActionTagId",
                table: "PositionActionMaps",
                column: "ActionTagId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentActionMaps_ActionTagId",
                table: "DepartmentActionMaps",
                column: "ActionTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentActionMaps_ActionTags_ActionTagId",
                table: "DepartmentActionMaps",
                column: "ActionTagId",
                principalTable: "ActionTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionActionMaps_ActionTags_ActionTagId",
                table: "PositionActionMaps",
                column: "ActionTagId",
                principalTable: "ActionTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActionMaps_ActionTags_ActionTagId",
                table: "UserActionMaps",
                column: "ActionTagId",
                principalTable: "ActionTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentActionMaps_ActionTags_ActionTagId",
                table: "DepartmentActionMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionActionMaps_ActionTags_ActionTagId",
                table: "PositionActionMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActionMaps_ActionTags_ActionTagId",
                table: "UserActionMaps");

            migrationBuilder.DropTable(
                name: "ActionTags");

            migrationBuilder.DropIndex(
                name: "IX_UserActionMaps_ActionTagId",
                table: "UserActionMaps");

            migrationBuilder.DropIndex(
                name: "IX_PositionActionMaps_ActionTagId",
                table: "PositionActionMaps");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentActionMaps_ActionTagId",
                table: "DepartmentActionMaps");

            migrationBuilder.DropColumn(
                name: "ActionTagId",
                table: "UserActionMaps");

            migrationBuilder.DropColumn(
                name: "ActionTagId",
                table: "PositionActionMaps");

            migrationBuilder.DropColumn(
                name: "ActionTagId",
                table: "DepartmentActionMaps");

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ActionName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ControllerName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentActionMaps_Actions_ActionId",
                table: "DepartmentActionMaps",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionActionMaps_Actions_ActionId",
                table: "PositionActionMaps",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActionMaps_Actions_ActionId",
                table: "UserActionMaps",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
