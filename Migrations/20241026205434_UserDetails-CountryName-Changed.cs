using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HET_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class UserDetailsCountryNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "County",
                table: "UserDetails",
                newName: "Country");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "UserDetails",
                newName: "County");
        }
    }
}
