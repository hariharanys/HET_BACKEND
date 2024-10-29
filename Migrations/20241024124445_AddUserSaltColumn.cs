using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HET_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSaltColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_userName",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salt",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_userName",
                table: "Users",
                column: "userName",
                unique: true);
        }
    }
}
