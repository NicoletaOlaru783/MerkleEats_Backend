using Microsoft.EntityFrameworkCore.Migrations;

namespace MerkleKitchenApp_V2.Migrations
{
    public partial class DeleteFieldTv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Settings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "End",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
