using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CityWeather.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    cityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityName = table.Column<string>(maxLength: 250, nullable: true),
                    counteryName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.cityId);
                });

            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityId = table.Column<int>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    temperature = table.Column<decimal>(nullable: false),
                    humidity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "cityId", "cityName", "counteryName" },
                values: new object[] { 1, "Amsterdam", "Netherlands" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "WeatherData");
        }
    }
}
