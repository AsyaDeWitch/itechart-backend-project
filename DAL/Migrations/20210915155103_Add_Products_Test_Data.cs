using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Add_Products_Test_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "Name", "Platform", "TotalRating" },
                values: new object[,]
                {
                    { 1, new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3: Wild Hunt", 0, 9.3000000000000007 },
                    { 23, new DateTime(2016, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Elder Scrolls V: Skyrim", 7, 7.7000000000000002 },
                    { 22, new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Elder Scrolls V: Skyrim", 3, 9.5999999999999996 },
                    { 21, new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Elder Scrolls V: Skyrim", 6, 9.1999999999999993 },
                    { 20, new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Elder Scrolls V: Skyrim", 0, 9.4000000000000004 },
                    { 19, new DateTime(2018, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red Dead Redemption 2", 0, 9.3000000000000007 },
                    { 18, new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red Dead Redemption 2", 4, 9.6999999999999993 },
                    { 17, new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red Dead Redemption 2", 7, 9.6999999999999993 },
                    { 16, new DateTime(2008, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "World of Warcraft: Wrath of the Lich King", 1, 9.0999999999999996 },
                    { 15, new DateTime(2008, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "World of Warcraft: Wrath of the Lich King", 0, 9.0999999999999996 },
                    { 14, new DateTime(2015, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V", 0, 9.5999999999999996 },
                    { 24, new DateTime(2016, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Elder Scrolls V: Skyrim", 4, 8.1999999999999993 },
                    { 13, new DateTime(2014, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V", 4, 9.6999999999999993 },
                    { 11, new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V", 3, 9.6999999999999993 },
                    { 10, new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V", 6, 9.6999999999999993 },
                    { 9, new DateTime(1999, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heroes of Might and Magic III", 2, 9.1999999999999993 },
                    { 8, new DateTime(1999, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heroes of Might and Magic III", 1, 9.1999999999999993 },
                    { 7, new DateTime(1999, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heroes of Might and Magic III", 0, 9.1999999999999993 },
                    { 6, new DateTime(2009, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Sims 3", 1, 8.5999999999999996 },
                    { 5, new DateTime(2009, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Sims 3", 0, 8.5999999999999996 },
                    { 4, new DateTime(2019, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3: Wild Hunt", 9, 8.5 },
                    { 3, new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3: Wild Hunt", 4, 9.0999999999999996 },
                    { 2, new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3: Wild Hunt", 7, 9.1999999999999993 },
                    { 12, new DateTime(2014, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V", 7, 9.6999999999999993 },
                    { 25, new DateTime(2017, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Elder Scrolls V: Skyrim", 9, 8.4000000000000004 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);
        }
    }
}
