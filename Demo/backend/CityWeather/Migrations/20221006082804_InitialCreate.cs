using Microsoft.EntityFrameworkCore.Migrations;

namespace CityWeather.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    cityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cityName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    counteryName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.cityId);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "cityId", "cityName", "counteryName" },
                values: new object[] { 1, "Amsterdam", "Netherlands" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "cityId", "cityName", "counteryName" },
                values: new object[] { 2, "Paris", "France" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "cityId", "cityName", "counteryName" },
                values: new object[] { 3, "Cairo", "Egypt" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
