using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Migrations
{
    public partial class Types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ads",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Ads");

            migrationBuilder.RenameTable(
                name: "Ads",
                newName: "Smartphones");

            migrationBuilder.AlterColumn<double>(
                name: "ScreenSize",
                table: "Smartphones",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RAM",
                table: "Smartphones",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BatteryCapacity",
                table: "Smartphones",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Smartphones",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Smartphones",
                table: "Smartphones",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Laptops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    ScreenSize = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Processor = table.Column<string>(nullable: true),
                    RAM = table.Column<int>(nullable: false),
                    Videocard = table.Column<string>(nullable: true),
                    DriveVolume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videocards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    MemorySize = table.Column<int>(nullable: false),
                    MemoryType = table.Column<string>(nullable: true),
                    MemoryBusWidth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videocards", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Laptops");

            migrationBuilder.DropTable(
                name: "Videocards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Smartphones",
                table: "Smartphones");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Smartphones");

            migrationBuilder.RenameTable(
                name: "Smartphones",
                newName: "Ads");

            migrationBuilder.AlterColumn<double>(
                name: "ScreenSize",
                table: "Ads",
                type: "float",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "RAM",
                table: "Ads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BatteryCapacity",
                table: "Ads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Ads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ads",
                table: "Ads",
                column: "Id");
        }
    }
}
