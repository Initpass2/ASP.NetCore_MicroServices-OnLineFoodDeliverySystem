using Microsoft.EntityFrameworkCore.Migrations;

namespace OMF.RestaurantManagement.Migrations
{
    public partial class dbmodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menus_cuisines_CuisineId",
                table: "menus");

            migrationBuilder.DropIndex(
                name: "IX_menus_CuisineId",
                table: "menus");

            migrationBuilder.DropColumn(
                name: "CuisineId",
                table: "menus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuisineId",
                table: "menus",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_menus_CuisineId",
                table: "menus",
                column: "CuisineId");

            migrationBuilder.AddForeignKey(
                name: "FK_menus_cuisines_CuisineId",
                table: "menus",
                column: "CuisineId",
                principalTable: "cuisines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
