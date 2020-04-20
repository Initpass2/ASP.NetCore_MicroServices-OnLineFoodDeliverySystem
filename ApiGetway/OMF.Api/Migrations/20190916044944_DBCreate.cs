using System;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMF.Api.Migrations
{
    public partial class DBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MasterRestaurantID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<IPoint>(nullable: false),
                    Cuisine = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Budget = table.Column<int>(nullable: false),
                    Rating = table.Column<decimal>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MasterReviewId = table.Column<int>(nullable: false),
                    ReviewText = table.Column<string>(nullable: true),
                    Rating = table.Column<decimal>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    RestaurantId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "restaurants");

            migrationBuilder.DropTable(
                name: "reviews");
        }
    }
}
