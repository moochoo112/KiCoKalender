using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class userfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "User",
                newName: "userLastName");

            migrationBuilder.AddColumn<string>(
                name: "userFirstName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userFirstName",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "userLastName",
                table: "User",
                newName: "userName");
        }
    }
}
