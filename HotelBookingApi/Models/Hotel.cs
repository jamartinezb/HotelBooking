using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Hotel
{
    [BindNever]
    [JsonIgnore]
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Ubicacion { get; set; }
    public bool Estado { get; set; }

    public List<Habitacion> Habitaciones { get; set; }

    [BindNever]
    [JsonIgnore]
    [NotMapped]
    [ValidateNever]
    public List<Reservacion> Reservaciones { get; set; }

    public Hotel()
    {
        Habitaciones = new List<Habitacion>(); 
        Reservaciones = new List<Reservacion>();
    }
}
