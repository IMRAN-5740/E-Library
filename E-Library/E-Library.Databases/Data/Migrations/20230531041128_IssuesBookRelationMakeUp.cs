using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Library.Data.Migrations
{
    public partial class IssuesBookRelationMakeUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssuesBooks_Categories_BookId",
                table: "IssuesBooks");

            migrationBuilder.AddColumn<string>(
                name: "BookCode",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_IssuesBooks_Books_BookId",
                table: "IssuesBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssuesBooks_Books_BookId",
                table: "IssuesBooks");

            migrationBuilder.DropColumn(
                name: "BookCode",
                table: "Books");

            migrationBuilder.AddForeignKey(
                name: "FK_IssuesBooks_Categories_BookId",
                table: "IssuesBooks",
                column: "BookId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
