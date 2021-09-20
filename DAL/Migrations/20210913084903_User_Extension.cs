using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class User_Extension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressDeliveryId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    HouseBuilding = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "-"),
                    EntranceNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    FlatNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressDeliveryId",
                table: "AspNetUsers",
                column: "AddressDeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_AddressDeliveryId",
                table: "AspNetUsers",
                column: "AddressDeliveryId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_AddressDeliveryId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressDeliveryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AddressDeliveryId",
                table: "AspNetUsers");
        }
    }
}
