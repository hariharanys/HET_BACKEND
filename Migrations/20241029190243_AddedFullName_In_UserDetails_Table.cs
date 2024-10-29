using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HET_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddedFullName_In_UserDetails_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "UserDetails",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "UserDetails");
        }
    }
}
