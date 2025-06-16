using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGAppDb.Migrations
{
    /// <inheritdoc />
    public partial class ChangedGTAVLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Grand Theft Auto V",
                column: "LogoUrl",
                value: "https://cdn2.steamgriddb.com/logo_thumb/e5b294b70c9647dcf804d7baa1903918.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Grand Theft Auto V",
                column: "LogoUrl",
                value: "https://media-rockstargames-com.akamaized.net/mfe6/prod/__common/img/732efc56393d89076732e76b0a2b55b2.svg");
        }
    }
}
