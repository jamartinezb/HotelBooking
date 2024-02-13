
namespace HotelBookingApi.Interfaces
{
    public interface IHabitacionRepository
    {
        Task<Habitacion> CreateAsync(Habitacion habitacion);
    }
}
