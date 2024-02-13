namespace HotelBookingApi.Interfaces
{
    public interface IHotelRepository
    {
        Task<Hotel> CreateAsync(Hotel hotel);
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel> GetByIdAsync(int id);
        Task UpdateAsync(Hotel hotel);
        Task DeleteAsync(int id);
    }
}
