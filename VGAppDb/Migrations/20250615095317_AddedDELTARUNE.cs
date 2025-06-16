using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGAppDb.Migrations
{
    /// <inheritdoc />
    public partial class AddedDELTARUNE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "BackgroundUrl", "Description", "LogoUrl", "PosterUrl", "PriceUSD", "ReleaseYear" },
                values: new object[] { "DELTARUNE", "https://deltarune.com/assets/images/bg.gif", "DELTARUNE! The RPG game where your choices don't matter! ", "https://deltarune.com/assets/images/logo.png", "https://deltarune.com/assets/images/key-art.gif", 25m, 2015 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "DELTARUNE");
        }
    }
}
