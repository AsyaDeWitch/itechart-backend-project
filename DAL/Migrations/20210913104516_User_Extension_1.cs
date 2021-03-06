using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class User_Extension_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_AddressDeliveryId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AddressDeliveryId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_AddressDeliveryId",
                table: "AspNetUsers",
                column: "AddressDeliveryId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_AddressDeliveryId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AddressDeliveryId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_AddressDeliveryId",
                table: "AspNetUsers",
                column: "AddressDeliveryId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
