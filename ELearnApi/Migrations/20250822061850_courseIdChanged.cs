using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearnApi.Migrations
{
    /// <inheritdoc />
    public partial class courseIdChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cards",
                newName: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Cards",
                newName: "Id");
        }
    }
}
