using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xyz.lsyyy.Verification.Data.Migrations
{
    public partial class Add_SuperiorPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SuperiorPositionId",
                table: "Positions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SuperiorPositionId",
                table: "Positions",
                column: "SuperiorPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Positions_SuperiorPositionId",
                table: "Positions",
                column: "SuperiorPositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Positions_SuperiorPositionId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_SuperiorPositionId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SuperiorPositionId",
                table: "Positions");
        }
    }
}
