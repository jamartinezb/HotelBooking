using HotelBookingApi.Data;
using HotelBookingApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using HotelBookingApi.DTOs;
using Microsoft.OpenApi.Models;
using RestSharp;
using RestSharp.Authenticators;
using HotelBookingApi.Services;

namespace HotelBookingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelesController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly EmailService _emailService;

        public HotelesController(IHotelRepository hotelRepository, ApplicationDbContext dbContext, EmailService emailService)
        {
            _hotelRepository = hotelRepository;
            _dbContext = dbContext;
            _emailService = emailService;
        }


        //crear hotel con cuartos
        [HttpPost]
        public async Task<ActionResult<Hotel>> CreateHotel(HotelCreateDto hotelDto)
        {
            if (hotelDto == null)
            {
                return BadRequest("Datos del hotel inválidos.");
            }

            var hotel = new Hotel
            {
                Nombre = hotelDto.Nombre,
                Ubicacion = hotelDto.Ubicacion,
                Estado = hotelDto.Estado,
                Habitaciones = hotelDto.Habitaciones.Select(h => new Habitacion
                {
                    Tipo = h.Tipo,
                    Nombre = h.Nombre,
                    Capacidad = h.Capacidad,
                    CostoBase = h.CostoBase,
                    Impuestos = h.Impuestos,
                    Estado = h.Estado
                }).ToList()
            };

            try
            {
                var createdHotel = await _hotelRepository.CreateAsync(hotel);
                return CreatedAtAction(nameof(GetHotel), new { id = createdHotel.Id }, createdHotel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al guardar los datos: " + ex.Message);
            }
        }

        //consultarHoteles
        [HttpGet("/consultarHoteles/{id}")]
        public async Task<ActionResult<HotelDetalleDto>> GetHotel(int id)
        {
            var hotel = await _dbContext.Hoteles
                .Include(h => h.Habitaciones)
                .Select(h => new HotelDetalleDto
                {
                    Id = h.Id,
                    Nombre = h.Nombre,
                    Ubicacion = h.Ubicacion,
                    Estado = h.Estado,
                    Habitaciones = h.Habitaciones.Select(hab => new HabitacionDetalleDto
                    {
                        Id = hab.Id,
                        Tipo = hab.Tipo,
                        Nombre = hab.Nombre,
                        Capacidad = hab.Capacidad,
                        CostoBase = hab.CostoBase,
                        Impuestos = hab.Impuestos,
                        Estado = hab.Estado
                    }).ToList()
                })
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }


        //asignarHabitacionHotel
        [HttpPost("/asignarHabitacionHotel/{hotelId}")]
        public async Task<ActionResult> AsignarHabitacion(int hotelId, HabitacionCreateDto habitacionDto)
        {
            var hotel = await _dbContext.Hoteles.Include(h => h.Habitaciones).FirstOrDefaultAsync(h => h.Id == hotelId);
            if (hotel == null)
            {
                return NotFound("Hotel no encontrado.");
            }

            var nuevaHabitacion = new Habitacion
            {
                Tipo = habitacionDto.Tipo,
                Nombre = habitacionDto.Nombre,
                Capacidad = habitacionDto.Capacidad,
                CostoBase = habitacionDto.CostoBase,
                Impuestos = habitacionDto.Impuestos,
                Estado = habitacionDto.Estado
            };

            hotel.Habitaciones.Add(nuevaHabitacion);

            try
            {
                await _dbContext.SaveChangesAsync();
                return Ok("Habitación asignada al hotel correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al asignar la habitación al hotel: " + ex.Message);
            }
        }


        //modificar los valores de cada habitación
        [HttpPut("{hotelId}/modificarHabitaciones/{habitacionId}")]
        public async Task<ActionResult> UpdateHabitacion(int hotelId, int habitacionId, HabitacionUpdateDto habitacionDto)
        {
            var hotel = await _dbContext.Hoteles.Include(h => h.Habitaciones).FirstOrDefaultAsync(h => h.Id == hotelId);
            if (hotel == null)
            {
                return NotFound("Hotel no encontrado.");
            }

            var habitacionExistente = hotel.Habitaciones.FirstOrDefault(h => h.Id == habitacionId);
            if (habitacionExistente == null)
            {
                return NotFound("Habitación no encontrada.");
            }

            // Actualizar los valores de la habitación existente con los valores proporcionados en el DTO
            habitacionExistente.Tipo = habitacionDto.Tipo;
            habitacionExistente.Nombre = habitacionDto.Nombre;
            habitacionExistente.Capacidad = habitacionDto.Capacidad;
            habitacionExistente.CostoBase = habitacionDto.CostoBase;
            habitacionExistente.Impuestos = habitacionDto.Impuestos;
            habitacionExistente.Estado = habitacionDto.Estado;

            try
            {
                await _dbContext.SaveChangesAsync();
                return Ok("Habitación actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al actualizar la habitación: " + ex.Message);
            }
        }


        //modificar los valores de cada hotel
        [HttpPut("/modificarHotel/{id}")]
        public async Task<ActionResult> UpdateHotel(int id, HotelForEditViewModel hotelViewModel)
        {
            var existingHotel = await _dbContext.Hoteles.FindAsync(id);
            if (existingHotel == null)
            {
                return NotFound("Hotel no encontrado.");
            }

            // Actualizar los valores del hotel existente con los valores proporcionados en el objeto hotel
            existingHotel.Nombre = hotelViewModel.Nombre;
            existingHotel.Ubicacion = hotelViewModel.Ubicacion;
            existingHotel.Estado = hotelViewModel.Estado;

            try
            {
                await _dbContext.SaveChangesAsync();
                return Ok("Hotel actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al actualizar el hotel: " + ex.Message);
            }
        }

        // habilitar o deshabilitar cada uno de los hoteles
        [HttpPut("/estadoHotel/{hotelId}")]
        public async Task<ActionResult> UpdateHotelState(int hotelId, bool estado)
        {
            var hotel = await _dbContext.Hoteles.FindAsync(hotelId);
            if (hotel == null)
            {
                return NotFound("Hotel no encontrado.");
            }

            hotel.Estado = estado;

            try
            {
                await _dbContext.SaveChangesAsync();
                return Ok($"Estado del hotel actualizado a: {(estado ? "Habilitado" : "Deshabilitado")}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al actualizar el estado del hotel: " + ex.Message);
            }
        }

        //habilitar o deshabilitar cada una de las habitaciones del hotel
        [HttpPut("{hotelId}/habitaciones/{habitacionId}/estado")]
        public async Task<ActionResult> UpdateHabitacionState(int hotelId, int habitacionId, [FromBody] HabitacionEstadoDto estadoDto)
        {
            var hotel = await _dbContext.Hoteles.Include(h => h.Habitaciones).FirstOrDefaultAsync(h => h.Id == hotelId);
            if (hotel == null)
            {
                return NotFound("Hotel no encontrado.");
            }

            var habitacion = hotel.Habitaciones.FirstOrDefault(h => h.Id == habitacionId);
            if (habitacion == null)
            {
                return NotFound("Habitación no encontrada.");
            }

            // Aquí usas el valor del estado dentro del DTO para actualizar la habitación
            habitacion.Estado = estadoDto.Estado;

            try
            {
                await _dbContext.SaveChangesAsync();
                return Ok($"Estado de la habitación {habitacionId} del hotel {hotelId} actualizado a: {(estadoDto.Estado ? "Habilitado" : "Deshabilitado")}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al actualizar el estado de la habitación: " + ex.Message);
            }
        }


        // listar cada una de las reservas realizadas en mis hoteles
        [HttpGet("reservas")]
        public async Task<ActionResult<IEnumerable<ReservacionDto>>> GetReservas()
        {
            try
            {
                var reservas = await _dbContext.Reservaciones
                    .Select(reserva => new ReservacionDto
                    {
                        Id = reserva.Id,
                        NombreCompleto = reserva.NombreCompleto,
                        FechaNacimiento = reserva.FechaNacimiento,
                        Genero = reserva.Genero,
                        TipoDocumento = reserva.TipoDocumento,
                        NumDocumento = reserva.NumDocumento,
                        Email = reserva.Email,
                        Telefono = reserva.Telefono,
                        NombreEmergencia = reserva.NombreEmergencia,
                        TelEmergencia = reserva.TelEmergencia,
                        FechaEntrada = reserva.FechaEntrada,
                        FechaSalida = reserva.FechaSalida,
                        CantPersonas = reserva.CantPersonas,
                        HotelId = reserva.HotelId,
                        HabitacionId = reserva.HabitacionId

                    })
                    .ToListAsync();

                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al obtener las reservas: " + ex.Message);
            }
        }


        //reservar habitacion en hotel
        [HttpPost("reservar")]
        public async Task<ActionResult> CrearReserva(ReservacionCreateDto reservaDto)
        {
            if (reservaDto == null)
            {
                return BadRequest("Datos de la reserva inválidos.");
            }

            // Validar si el hotel existe
            var hotelExistente = await _dbContext.Hoteles.FindAsync(reservaDto.HotelId);
            if (hotelExistente == null)
            {
                return NotFound($"El hotel con ID {reservaDto.HotelId} no existe.");
            }

            // Validar si la habitación existe y está disponible
            var habitacionExistente = await _dbContext.Habitaciones.FindAsync(reservaDto.HabitacionId);
            if (habitacionExistente == null || !habitacionExistente.Estado)
            {
                return NotFound($"La habitación con ID {reservaDto.HabitacionId} no existe o no está disponible.");
            }

            // Validar capacidad de la habitación
            if (reservaDto.CantPersonas > habitacionExistente.Capacidad)
            {
                return BadRequest("La cantidad de personas excede la capacidad de la habitación.");
            }
            // Validar si hay solapamiento de fechas, teniendo en cuenta el día completo
            bool solapamiento = await _dbContext.Reservaciones.AnyAsync(r =>
                r.HabitacionId == reservaDto.HabitacionId &&
                reservaDto.FechaEntrada <= r.FechaSalida &&
                reservaDto.FechaSalida >= r.FechaEntrada); 



            if (solapamiento)
            {
                return BadRequest("Ya existe una reserva para esta habitación en las fechas especificadas.");
            }

            // Crear la nueva reserva
            Reservacion nuevaReserva = new Reservacion
            {
                NombreCompleto = reservaDto.NombreCompleto,
                FechaNacimiento = reservaDto.FechaNacimiento,
                Genero = reservaDto.Genero,
                TipoDocumento = reservaDto.TipoDocumento,
                NumDocumento = reservaDto.NumDocumento,
                Email = reservaDto.Email,
                Telefono = reservaDto.Telefono,
                NombreEmergencia = reservaDto.NombreEmergencia,
                TelEmergencia = reservaDto.TelEmergencia,
                FechaEntrada = reservaDto.FechaEntrada,
                FechaSalida = reservaDto.FechaSalida,
                CantPersonas = reservaDto.CantPersonas,
                HotelId = reservaDto.HotelId, // Asignar HotelId
                HabitacionId = reservaDto.HabitacionId
            };

            try
            {
                var createdReserva = await _dbContext.Reservaciones.AddAsync(nuevaReserva);

                await _dbContext.SaveChangesAsync();
                // Preparar y enviar el correo electrónico de confirmación
                var subject = $"Confirmación de Reserva - {nuevaReserva.NombreCompleto}";
                var messageText = $"<p>Hola {nuevaReserva.NombreCompleto}, Tu reserva en el hotel desde el {nuevaReserva.FechaEntrada.ToShortDateString()} " +
                                  $"hasta el {nuevaReserva.FechaSalida.ToShortDateString()} ha sido confirmada.</p>";
                var emailResponse = _emailService.SendReservationEmailAsyncGoo(nuevaReserva.Email, subject, messageText);

                return CreatedAtAction(nameof(GetReservas), new { id = createdReserva.Entity.Id }, createdReserva.Entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al crear la reserva: " + ex.Message);
            }
        }

        //buscar hotel para reservar
        [HttpGet("buscar")]
        public async Task<ActionResult<List<HotelBusquedaDto>>> BuscarHoteles(DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas, string ciudadDestino)
        {
            var hotelesDisponibles = await _dbContext.Hoteles
                .Where(h => h.Ubicacion.ToLower() == ciudadDestino.ToLower() && h.Habitaciones.Any(hab => hab.Capacidad >= cantidadPersonas && hab.Estado))
                .Include(h => h.Habitaciones)
                    .ThenInclude(hab => hab.Reservaciones)
                .ToListAsync();

            var hotelesFiltradosDto = hotelesDisponibles
                .Where(hotel => hotel.Habitaciones.Any(habitacion => habitacion.Capacidad >= cantidadPersonas &&
                    habitacion.Estado && !habitacion.Reservaciones.Any(reservacion =>
                        fechaEntrada <= reservacion.FechaSalida && fechaSalida >= reservacion.FechaEntrada)))
                .Select(hotel => new HotelBusquedaDto
                {
                    Id = hotel.Id,
                    Nombre = hotel.Nombre,
                    Ubicacion = hotel.Ubicacion,
                    HabitacionesDisponibles = hotel.Habitaciones
                        .Where(habitacion => habitacion.Capacidad >= cantidadPersonas &&
                            habitacion.Estado && !habitacion.Reservaciones.Any(reservacion =>
                                fechaEntrada <= reservacion.FechaSalida && fechaSalida >= reservacion.FechaEntrada))
                        .Select(habitacion => new HabitacionBusquedaDto
                        {
                            Id = habitacion.Id,
                            Tipo = habitacion.Tipo,
                            Capacidad = habitacion.Capacidad,
                            CostoBase = habitacion.CostoBase,
                        }).ToList()
                }).ToList();

            if (!hotelesFiltradosDto.Any())
            {
                return NotFound("No se encontró disponibilidad en las fechas especificadas.");
            }

            return hotelesFiltradosDto;
        }



    }
}
