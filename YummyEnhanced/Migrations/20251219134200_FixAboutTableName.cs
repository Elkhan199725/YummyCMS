using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyEnhanced.Migrations
{
    /// <inheritdoc />
    public partial class FixAboutTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Aboutes",
                table: "Aboutes");

            migrationBuilder.RenameTable(
                name: "Aboutes",
                newName: "Abouts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abouts",
                table: "Abouts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Abouts",
                table: "Abouts");

            migrationBuilder.RenameTable(
                name: "Abouts",
                newName: "Aboutes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aboutes",
                table: "Aboutes",
                column: "Id");
        }
    }
}
