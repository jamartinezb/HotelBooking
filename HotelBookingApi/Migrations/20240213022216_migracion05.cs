using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingApi.Migrations
{
    public partial class migracion05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reservado",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservaciones_Habitaciones_HabitacionId",
                table: "Reservaciones");

            migrationBuilder.DropIndex(
                name: "IX_Reservaciones_HabitacionId",
                table: "Reservaciones");

            migrationBuilder.AddColumn<bool>(
                name: "Reservado",
                table: "Habitaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
