using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Migrations
{
    public partial class Test1stCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Ads",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BatteryCapacity",
                table: "Ads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Processor",
                table: "Ads",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RAM",
                table: "Ads",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ScreenSize",
                table: "Ads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "BatteryCapacity",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Processor",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "RAM",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "ScreenSize",
                table: "Ads");
        }
    }
}
