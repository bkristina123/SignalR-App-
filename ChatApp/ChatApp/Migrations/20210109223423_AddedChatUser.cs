using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Migrations
{
    public partial class AddedChatUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_AspNetUsers_UserId",
                table: "ChatUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatUsers",
                table: "ChatUsers");

            migrationBuilder.DropIndex(
                name: "IX_ChatUsers_ChatId",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChatUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ChatUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatUsers",
                table: "ChatUsers",
                columns: new[] { "ChatId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_AspNetUsers_UserId",
                table: "ChatUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_AspNetUsers_UserId",
                table: "ChatUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatUsers",
                table: "ChatUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ChatUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ChatUsers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatUsers",
                table: "ChatUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_ChatId",
                table: "ChatUsers",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_AspNetUsers_UserId",
                table: "ChatUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
