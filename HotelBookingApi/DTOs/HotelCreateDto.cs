using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.DTOs
{
    public class HotelCreateDto
    {
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public bool Estado { get; set; }
        public List<HabitacionCreateDto> Habitaciones { get; set; }
    }
    public class HotelDetalleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public bool Estado { get; set; }
        public List<HabitacionDetalleDto> Habitaciones { get; set; }
    }

    public class HotelForEditViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        public string Ubicacion { get; set; }

        public bool Estado { get; set; }
    }


    public class HabitacionDetalleDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public decimal CostoBase { get; set; }
        public decimal Impuestos { get; set; }
        public bool Estado { get; set; }
    }
    public class HabitacionCreateDto
    {
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public decimal CostoBase { get; set; }
        public decimal Impuestos { get; set; }
        public bool Estado { get; set; }
    }


    public class HabitacionUpdateDto
    {
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public decimal CostoBase { get; set; }
        public decimal Impuestos { get; set; }
        public bool Estado { get; set; }
    }

    public class HabitacionEstadoDto
    {
        public bool Estado { get; set; }
    }
    public class ReservacionDto
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
    }
    public class ReservacionCreateDto
    {
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
    }

    public class ReservacionUpdateDto : ReservacionCreateDto
    {
        public int Id { get; set; }
    }
    public class HotelBusquedaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public List<HabitacionBusquedaDto> HabitacionesDisponibles { get; set; }
    }

    public class HabitacionBusquedaDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public decimal CostoBase { get; set; }
        public decimal Impuestos { get; set; }
        public bool Estado { get; set; }
    }

}
