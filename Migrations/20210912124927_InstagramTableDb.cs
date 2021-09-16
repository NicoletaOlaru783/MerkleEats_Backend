using Microsoft.EntityFrameworkCore.Migrations;

namespace MerkleKitchenApp_V2.Migrations
{
    public partial class InstagramTableDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instagram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Access_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresIn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instagram", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instagram");
        }
    }
}
