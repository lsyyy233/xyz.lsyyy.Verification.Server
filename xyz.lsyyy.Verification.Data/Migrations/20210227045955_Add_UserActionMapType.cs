using Microsoft.EntityFrameworkCore.Migrations;

namespace xyz.lsyyy.Verification.Data.Migrations
{
    public partial class Add_UserActionMapType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessType",
                table: "UserActionMaps",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessType",
                table: "UserActionMaps");
        }
    }
}
