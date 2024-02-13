using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingApi.Migrations
{
    public partial class migracion04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservaciones_Habitaciones_HabitacionId",
                table: "Reservaciones");

            migrationBuilder.DropIndex(
                name: "IX_Reservaciones_HabitacionId",
                table: "Reservaciones");

            migrationBuilder.AddColumn<int>(
                name: "Capacidad",
                table: "Habitaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacidad",
                table: "Habitaciones");

            migrationBuilder.CreateIndex(
                name: "IX_Reservaciones_HabitacionId",
                table: "Reservaciones",
                column: "HabitacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservaciones_Habitaciones_HabitacionId",
                table: "Reservaciones",
                column: "HabitacionId",
                principalTable: "Habitaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
