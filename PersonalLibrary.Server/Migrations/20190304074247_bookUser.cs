using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalLibrary.Server.Migrations
{
    public partial class bookUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Userbook_Book_BookId",
                table: "Userbook");

            migrationBuilder.DropForeignKey(
                name: "FK_Userbook_UserAccess_Userid",
                table: "Userbook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Userbook",
                table: "Userbook");

            migrationBuilder.RenameTable(
                name: "Userbook",
                newName: "UserBook");

            migrationBuilder.RenameIndex(
                name: "IX_Userbook_Userid",
                table: "UserBook",
                newName: "IX_UserBook_Userid");

            migrationBuilder.RenameIndex(
                name: "IX_Userbook_BookId",
                table: "UserBook",
                newName: "IX_UserBook_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook",
                column: "UserbookId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_Book_BookId",
                table: "UserBook",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Bookid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_UserAccess_Userid",
                table: "UserBook",
                column: "Userid",
                principalTable: "UserAccess",
                principalColumn: "Userid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_Book_BookId",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_UserAccess_Userid",
                table: "UserBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook");

            migrationBuilder.RenameTable(
                name: "UserBook",
                newName: "Userbook");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_Userid",
                table: "Userbook",
                newName: "IX_Userbook_Userid");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_BookId",
                table: "Userbook",
                newName: "IX_Userbook_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Userbook",
                table: "Userbook",
                column: "UserbookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Userbook_Book_BookId",
                table: "Userbook",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Bookid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Userbook_UserAccess_Userid",
                table: "Userbook",
                column: "Userid",
                principalTable: "UserAccess",
                principalColumn: "Userid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
