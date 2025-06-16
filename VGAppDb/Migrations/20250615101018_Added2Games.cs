using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VGAppDb.Migrations
{
    /// <inheritdoc />
    public partial class Added2Games : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Grand Theft Auto V",
                column: "PriceUSD",
                value: 40m);

            migrationBuilder.InsertData(
                table: "Games",
                columns: ["Name", "BackgroundUrl", "Description", "LogoUrl", "PosterUrl", "PriceUSD", "ReleaseYear"],
                values: new object[,]
                {
                    { "Red Dead Redemption 2", "https://cdn2.steamgriddb.com/hero_thumb/81e5f81db77c596492e6f1a5a792ed53.jpg", "Winner of over 175 Game of the Year Awards and recipient of over 250 perfect scores, RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, on the run across America at the dawn of the modern age. ", "https://shared.steamstatic.com/store_item_assets/steam/apps/1174180/logo_2x.png?t=1671484934", "https://cdn2.steamgriddb.com/thumb/e746c3c588c51ad5efcc7125e3df662c.jpg", 40m, 2018 },
                    { "Terraria", "https://shared.steamstatic.com/store_item_assets/steam/apps/105600/library_hero.jpg?t=1666290502", "", "https://cdn2.steamgriddb.com/logo_thumb/43270821c3f3f838312dc462c8d920cc.webm", "https://cdn2.steamgriddb.com/thumb/86f045465e82c214dc5e68ba530546ba.jpg", 25m, 2011 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Red Dead Redemption 2");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Terraria");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Grand Theft Auto V",
                column: "PriceUSD",
                value: 25m);
        }
    }
}
