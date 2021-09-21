using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Product_Extension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "https://firebasestorage.googleapis.com/v0/b/lab-web-app-299ef.appspot.com/o/empty_background.jpg?alt=media&token=0ad4ed61-2a4e-4b48-be4b-d683282d5fc5e");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "https://firebasestorage.googleapis.com/v0/b/lab-web-app-299ef.appspot.com/o/empty_image.png?alt=media&token=fdbc866c-69d0-458e-a162-29a17eca00fe");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 10, "RPG", 19.989999999999998, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 5, "RPG", 19.989999999999998, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 7, "RPG", 19.989999999999998, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 8, "RPG", 19.989999999999998, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 8, "Simulation", 19.989999999999998, 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 13, "Simulation", 19.989999999999998, 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Count", "Genre", "Price" },
                values: new object[] { 6, "Turn-based strategy with RPG elements", 8.25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Count", "Genre", "Price" },
                values: new object[] { 3, "Turn-based strategy with RPG elements", 8.25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Count", "Genre", "Price" },
                values: new object[] { 1, "Turn-based strategy with RPG elements", 8.25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 6, "Action-adventure", 12.58, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 8, "Action-adventure", 12.58, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 12, "Action-adventure", 12.58, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 4, "Action-adventure", 12.58, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 2, "Action-adventure", 12.58, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 2, "RPG", 40.450000000000003, 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 3, "RPG", 40.450000000000003, 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 25, "Action-adventure", 34.990000000000002, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 2, "Action-adventure", 34.990000000000002, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 14, "Action-adventure", 34.990000000000002, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 3, "RPG", 7.9900000000000002, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 7, "RPG", 7.9900000000000002, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 5, "RPG", 7.9900000000000002, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 3, "RPG", 7.9900000000000002, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 5, "RPG", 7.9900000000000002, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Count", "Genre", "Price", "Rating" },
                values: new object[] { 4, "RPG", 7.9900000000000002, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Background",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");
        }
    }
}
