using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class Habitacion
{
    [BindNever]
    [JsonIgnore]
    public int Id { get; set; }
    public string Tipo { get; set; }
    public string Nombre { get; set; }
    public int Capacidad { get; set; }
    public decimal CostoBase { get; set; }
    public decimal Impuestos { get; set; }
    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual List<Reservacion> Reservaciones { get; set; } = new List<Reservacion>();
}
