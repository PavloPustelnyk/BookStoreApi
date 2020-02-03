using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Infrastructure.ApiContext.Migrations
{
    public partial class AddImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "BookImage",
                table: "Books",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BookCategory",
                table: "BookCategories",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "AuthorImage",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookImage",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorImage",
                table: "Authors");

            migrationBuilder.AlterColumn<string>(
                name: "BookCategory",
                table: "BookCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
