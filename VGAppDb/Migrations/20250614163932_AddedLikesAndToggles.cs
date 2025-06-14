using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGAppDb.Migrations
{
    /// <inheritdoc />
    public partial class AddedLikesAndToggles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCreated",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "GameUsers",
                columns: table => new
                {
                    GamesPlayedName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsersThatPlayedId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUsers", x => new { x.GamesPlayedName, x.UsersThatPlayedId });
                    table.ForeignKey(
                        name: "FK_GameUsers_AspNetUsers_UsersThatPlayedId",
                        column: x => x.UsersThatPlayedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUsers_Games_GamesPlayedName",
                        column: x => x.GamesPlayedName,
                        principalTable: "Games",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReviewLikes",
                columns: table => new
                {
                    LikedReviewsId = table.Column<int>(type: "int", nullable: false),
                    UsersThatLikedId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewLikes", x => new { x.LikedReviewsId, x.UsersThatLikedId });
                    table.ForeignKey(
                        name: "FK_ReviewLikes_AspNetUsers_UsersThatLikedId",
                        column: x => x.UsersThatLikedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewLikes_Reviews_LikedReviewsId",
                        column: x => x.LikedReviewsId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GameUsers_UsersThatPlayedId",
                table: "GameUsers",
                column: "UsersThatPlayedId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewLikes_UsersThatLikedId",
                table: "ReviewLikes",
                column: "UsersThatLikedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameUsers");

            migrationBuilder.DropTable(
                name: "ReviewLikes");

            migrationBuilder.DropColumn(
                name: "TimeCreated",
                table: "AspNetUsers");

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

            migrationBuilder.CreateIndex(
                name: "IX_ReviewUser_LikedReviewsId",
                table: "ReviewUser",
                column: "LikedReviewsId");
        }
    }
}
