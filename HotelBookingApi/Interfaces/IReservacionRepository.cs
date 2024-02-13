namespace HotelBookingApi.Interfaces
{
    public interface IReservacionRepository
    {
        Task<Reservacion> CreateAsync(Reservacion reservacion);
    }
}

