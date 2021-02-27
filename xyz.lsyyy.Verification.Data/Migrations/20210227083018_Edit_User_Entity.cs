using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xyz.lsyyy.Verification.Data.Migrations
{
    public partial class Edit_User_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "PositionId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ActionTagId",
                table: "UserActionMaps",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ActionTagId",
                table: "PositionActionMaps",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ActionTagId",
                table: "DepartmentActionMaps",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentActionMaps_ActionTags_ActionTagId",
                table: "DepartmentActionMaps",
                column: "ActionTagId",
                principalTable: "ActionTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionActionMaps_ActionTags_ActionTagId",
                table: "PositionActionMaps",
                column: "ActionTagId",
                principalTable: "ActionTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActionMaps_ActionTags_ActionTagId",
                table: "UserActionMaps",
                column: "ActionTagId",
                principalTable: "ActionTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users",
                column: "PositionId",
                principalTable: "Positions",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "PositionId",
                table: "Users",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ActionTagId",
                table: "UserActionMaps",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ActionTagId",
                table: "PositionActionMaps",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ActionTagId",
                table: "DepartmentActionMaps",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
