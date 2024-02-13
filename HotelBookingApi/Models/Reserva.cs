using System;
using System.Text.Json.Serialization;

public class Reservacion
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public char Genero { get; set; }
    public string TipoDocumento { get; set; }
    public int NumDocumento { get; set; }
    public string Email { get; set; }
    public int Telefono { get; set; }
    public string NombreEmergencia { get; set; }
    public int TelEmergencia { get; set; }
    public DateTime FechaEntrada { get; set; }
    public DateTime FechaSalida { get; set; }
    public int CantPersonas { get; set; }
    public int HotelId { get; set; }
    public int HabitacionId { get; set; }

    [JsonIgnore]
    public Habitacion Habitacion { get; set; }
}
