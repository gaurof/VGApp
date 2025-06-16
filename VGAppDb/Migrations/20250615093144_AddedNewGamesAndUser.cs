using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGAppDb.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewGamesAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "BackgroundUrl", "Description", "LogoUrl", "PosterUrl", "PriceUSD", "ReleaseYear" },
                values: new object[] { "Counter-Strike", "https://cdn2.steamgriddb.com/hero_thumb/1be3614ec5d67a9fe3fd389516f369ea.jpg", "Play the world's number 1 online action game. Engage in an incredibly realistic brand of terrorist warfare in this wildly popular team-based game. Ally with teammates to complete strategic missions. Take out enemy sites. ", "https://cdn2.steamgriddb.com/logo_thumb/13d429db192fbc7b5cabf9b936cf78e1.png", "https://cdn2.steamgriddb.com/thumb/6bf8cff2494ff41052ac8474df638cdb.jpg", 5m, 2000 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Counter-Strike");
        }
    }
}
