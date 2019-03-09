using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonalLibrary.Server.Migrations
{
    public partial class customIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_UserAccess_Userid",
                table: "UserBook");

            migrationBuilder.DropTable(
                name: "UserAccess");

            migrationBuilder.DropIndex(
                name: "IX_UserBook_Userid",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "UserBook");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "UserBook",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAppIdentityId",
                table: "UserBook",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RealName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_OwnerID",
                table: "UserBook",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_UserAppIdentityId",
                table: "UserBook",
                column: "UserAppIdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_OwnerID",
                table: "UserBook",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_UserAppIdentityId",
                table: "UserBook",
                column: "UserAppIdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_OwnerID",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_UserAppIdentityId",
                table: "UserBook");

            migrationBuilder.DropIndex(
                name: "IX_UserBook_OwnerID",
                table: "UserBook");

            migrationBuilder.DropIndex(
                name: "IX_UserBook_UserAppIdentityId",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "UserAppIdentityId",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "RealName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "UserBook",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "UserBook",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAccess",
                columns: table => new
                {
                    Userid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccess", x => x.Userid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_Userid",
                table: "UserBook",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_UserAccess_Userid",
                table: "UserBook",
                column: "Userid",
                principalTable: "UserAccess",
                principalColumn: "Userid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
