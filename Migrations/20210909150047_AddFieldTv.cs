using Microsoft.EntityFrameworkCore.Migrations;

namespace MerkleKitchenApp_V2.Migrations
{
    public partial class AddFieldTv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Finish",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finish",
                table: "Settings");
        }
    }
}
