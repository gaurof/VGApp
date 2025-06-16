using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGAppDb.Migrations
{
    /// <inheritdoc />
    public partial class AddedTF2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "BackgroundUrl", "Description", "LogoUrl", "PosterUrl", "PriceUSD", "ReleaseYear" },
                values: new object[] { "Team Fortress 2", "https://shared.steamstatic.com/store_item_assets/steam/apps/440/library_hero.jpg?t=1745368576", "After 9 years in development.", "https://shared.steamstatic.com/store_item_assets/steam/apps/440/logo.png?t=1745368576", "https://cdn2.steamgriddb.com/thumb/2eaa17f7324d93370a43a7b8d55d038e.jpg", 0m, 2009 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Team Fortress 2");
        }
    }
}
