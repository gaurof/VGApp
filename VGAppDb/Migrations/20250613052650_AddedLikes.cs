using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VGAppDb.Migrations
{
    /// <inheritdoc />
    public partial class AddedLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "UserId",
                keyValue: null,
                column: "UserId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReviewUser",
                columns: table => new
                {
                    LikedByUsersId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LikedReviewsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewUser", x => new { x.LikedByUsersId, x.LikedReviewsId });
                    table.ForeignKey(
                        name: "FK_ReviewUser_AspNetUsers_LikedByUsersId",
                        column: x => x.LikedByUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewUser_Reviews_LikedReviewsId",
                        column: x => x.LikedReviewsId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "BackgroundUrl", "Description", "LogoUrl", "PosterUrl", "PriceUSD", "ReleaseYear" },
                values: new object[,]
                {
                    { "DOOM", "https://shared.steamstatic.com/store_item_assets/steam/apps/379720/library_hero_2x.jpg?t=1573231983", "Fight like hell", "https://shared.steamstatic.com/store_item_assets/steam/apps/379720/logo_2x.png?t=1573231983", "https://cdn2.steamgriddb.com/thumb/775974bd62116bc3d3b2c51b04192f0c.png", 40m, 2016 },
                    { "Minecraft", "https://cdn2.steamgriddb.com/hero_thumb/ae93f6696a2a89b67aa6fb45092eded7.jpg", "Also try terraria!", "https://cdn2.steamgriddb.com/logo_thumb/90915208c601cc8c86ad01250ee90c12.png", "https://cdn2.steamgriddb.com/thumb/782c68199db381ee34a277258c28c89c.jpg", 20m, 2011 },
                    { "Undertale", "https://shared.steamstatic.com/store_item_assets/steam/apps/391540/library_hero.jpg?t=1579095961", "UNDERTALE! The RPG game where you don't have to destroy anyone. ", "https://shared.steamstatic.com/store_item_assets/steam/apps/391540/logo.png?t=1579095961", "https://cdn2.steamgriddb.com/thumb/14ec86d482ff9638392a061bfa431a1a.jpg", 20m, 2015 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewUser_LikedReviewsId",
                table: "ReviewUser",
                column: "LikedReviewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "ReviewUser");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "DOOM");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Minecraft");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Undertale");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
