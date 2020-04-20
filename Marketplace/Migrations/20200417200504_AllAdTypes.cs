using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Migrations
{
    public partial class AllAdTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drives",
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
                    Subscribers = table.Column<string>(nullable: true),
                    IsFreezed = table.Column<bool>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    RPM = table.Column<int>(nullable: false),
                    FormFactor = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monitors",
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
                    Subscribers = table.Column<string>(nullable: true),
                    IsFreezed = table.Column<bool>(nullable: false),
                    ScreenSize = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Resolution = table.Column<string>(nullable: true),
                    MatrixType = table.Column<string>(nullable: true),
                    RefreshRate = table.Column<int>(nullable: false),
                    AspectRatio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processors",
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
                    Subscribers = table.Column<string>(nullable: true),
                    IsFreezed = table.Column<bool>(nullable: false),
                    CoresAmount = table.Column<int>(nullable: false),
                    ClockSpeed = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAMs",
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
                    Subscribers = table.Column<string>(nullable: true),
                    IsFreezed = table.Column<bool>(nullable: false),
                    TotalCapacity = table.Column<int>(nullable: false),
                    ModulesAmount = table.Column<int>(nullable: false),
                    MemoryType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drives");

            migrationBuilder.DropTable(
                name: "Monitors");

            migrationBuilder.DropTable(
                name: "Processors");

            migrationBuilder.DropTable(
                name: "RAMs");
        }
    }
}
