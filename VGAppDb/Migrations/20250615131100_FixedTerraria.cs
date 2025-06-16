using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGAppDb.Migrations
{
    /// <inheritdoc />
    public partial class FixedTerraria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Terraria",
                columns: new[] { "Description", "LogoUrl", "PosterUrl", "PriceUSD" },
                values: new object[] { "Also try Minecraft!", "https://shared.steamstatic.com/store_item_assets/steam/apps/105600/logo_2x.png?t=1666290502", "https://images-ext-1.discordapp.net/external/ftJWBEe_E9ZCBdz6EEqxSXK4b5r_9zFTEimtI9KII7Q/https/cdn2.steamgriddb.com/thumb/301c8008a981254f98950cebef344b58.jpg?format=webp", 5m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Terraria",
                columns: new[] { "Description", "LogoUrl", "PosterUrl", "PriceUSD" },
                values: new object[] { "", "https://cdn2.steamgriddb.com/logo_thumb/43270821c3f3f838312dc462c8d920cc.webm", "https://cdn2.steamgriddb.com/thumb/86f045465e82c214dc5e68ba530546ba.jpg", 25m });
        }
    }
}
