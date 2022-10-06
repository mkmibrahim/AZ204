using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CityWeather.Migrations
{
    public partial class AddedWeatherDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "cityId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "cityId",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cityId = table.Column<int>(type: "INTEGER", nullable: false),
                    time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    temperature = table.Column<decimal>(type: "TEXT", nullable: false),
                    humidity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherData");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "cityId", "cityName", "counteryName" },
                values: new object[] { 2, "Paris", "France" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "cityId", "cityName", "counteryName" },
                values: new object[] { 3, "Cairo", "Egypt" });
        }
    }
}
