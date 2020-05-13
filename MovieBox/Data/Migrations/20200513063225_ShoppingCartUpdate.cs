using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieBox.Data.Migrations
{
    public partial class ShoppingCartUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "ShoppingCart");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "ShoppingCart");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "ShoppingCart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
