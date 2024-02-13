using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingApi.Migrations
{
    public partial class migracion03 : Migration
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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Reservado",
                table: "Habitaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Reservaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumDocumento = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    NombreEmergencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelEmergencia = table.Column<int>(type: "int", nullable: false),
                    FechaEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantPersonas = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    HabitacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservaciones_Habitaciones_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservaciones_HabitacionId",
                table: "Reservaciones",
                column: "HabitacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habitaciones_Hoteles_HotelId",
                table: "Habitaciones",
                column: "HotelId",
                principalTable: "Hoteles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitaciones_Hoteles_HotelId",
                table: "Habitaciones");

            migrationBuilder.DropTable(
                name: "Reservaciones");

            migrationBuilder.DropColumn(
                name: "Reservado",
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
    }
}
