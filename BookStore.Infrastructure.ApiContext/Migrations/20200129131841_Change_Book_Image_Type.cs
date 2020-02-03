using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Infrastructure.ApiContext.Migrations
{
    public partial class Change_Book_Image_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BookImage",
                table: "Books",
                maxLength: 5242880,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldMaxLength: 5242880,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "BookImage",
                table: "Books",
                type: "varbinary(max)",
                maxLength: 5242880,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5242880,
                oldNullable: true);
        }
    }
}
