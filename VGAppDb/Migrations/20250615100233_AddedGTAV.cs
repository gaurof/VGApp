using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGAppDb.Migrations
{
    /// <inheritdoc />
    public partial class AddedGTAV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "DELTARUNE",
                columns: new[] { "Description", "ReleaseYear" },
                values: new object[] { "DELTARUNE! The RPG game where your choices don't matter. ", 2025 });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "BackgroundUrl", "Description", "LogoUrl", "PosterUrl", "PriceUSD", "ReleaseYear" },
                values: new object[] { "Grand Theft Auto V", "https://images.steamusercontent.com/ugc/11669731338331342254/D8FF2435AC1815F69543C8DEE34D15D52399A3DA/?imw=2048&imh=1152&ima=fit&impolicy=Letterbox&imcolor=%23000000&letterbox=true", "Grand Theft Auto V for PC offers players the option to explore the award-winning world of Los Santos and Blaine County in resolutions of up to 4k and beyond, as well as the chance to experience the game running at 60 frames per second.", "https://media-rockstargames-com.akamaized.net/mfe6/prod/__common/img/732efc56393d89076732e76b0a2b55b2.svg", "https://cdn2.steamgriddb.com/thumb/86f045465e82c214dc5e68ba530546ba.jpg", 25m, 2013 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Grand Theft Auto V");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "DELTARUNE",
                columns: new[] { "Description", "ReleaseYear" },
                values: new object[] { "UNDERTALE! The RPG game where you don't have to destroy anyone. ", 2015 });
        }
    }
}
