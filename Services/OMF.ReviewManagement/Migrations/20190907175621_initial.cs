using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMF.ReviewManagement.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(nullable: false),
                    ReviewText = table.Column<string>(nullable: false),
                    ModeratorTrackingId = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: false),
                    ResturentName = table.Column<string>(nullable: false),
                    ResturentID = table.Column<string>(nullable: true),
                    Rating = table.Column<decimal>(nullable: false),
                    IsManualReviewRequired = table.Column<bool>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.ReviewId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ReviewId_OrderId",
                table: "reviews",
                columns: new[] { "ReviewId", "OrderId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");
        }
    }
}
