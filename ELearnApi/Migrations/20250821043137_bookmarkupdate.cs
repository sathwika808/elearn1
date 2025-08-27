using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearnApi.Migrations
{
    /// <inheritdoc />
    public partial class bookmarkupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "answer",
                table: "BookMarks");

            migrationBuilder.DropColumn(
                name: "question",
                table: "BookMarks");

            migrationBuilder.AlterColumn<long>(
                name: "CardId",
                table: "BookMarks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BookMarks_CardId",
                table: "BookMarks",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookMarks_Cards_CardId",
                table: "BookMarks",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookMarks_Cards_CardId",
                table: "BookMarks");

            migrationBuilder.DropIndex(
                name: "IX_BookMarks_CardId",
                table: "BookMarks");

            migrationBuilder.AlterColumn<string>(
                name: "CardId",
                table: "BookMarks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "answer",
                table: "BookMarks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "question",
                table: "BookMarks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
