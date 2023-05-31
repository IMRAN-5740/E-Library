using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Library.Data.Migrations
{
    public partial class AuthorDescriptionTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Authors");
        }
    }
}
