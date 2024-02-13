using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingApi.Migrations
{
    public partial class migracion02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitaciones_Hoteles_HotelId",
                table: "Habitaciones");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Habitaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Habitaciones_Hoteles_HotelId",
                table: "Habitaciones",
                column: "HotelId",
                principalTable: "Hoteles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitaciones_Hoteles_HotelId",
                table: "Habitaciones");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Habitaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Habitaciones_Hoteles_HotelId",
                table: "Habitaciones",
                column: "HotelId",
                principalTable: "Hoteles",
                principalColumn: "Id");
        }
    }
}
