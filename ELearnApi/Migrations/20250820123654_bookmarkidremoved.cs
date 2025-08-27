using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearnApi.Migrations
{
    /// <inheritdoc />
    public partial class bookmarkidremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookmarkEntryId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Courses",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "description");

            migrationBuilder.AddColumn<string>(
                name: "BookmarkEntryId",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
