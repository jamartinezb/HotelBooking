using HotelBookingApi.Interfaces;
using HotelBookingApi.Data;
using System.Data.Entity;

namespace HotelBookingApi.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> CreateAsync(Hotel hotel)
        {
            _context.Hoteles.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hoteles.ToListAsync();
        }

        public async Task<Hotel> GetByIdAsync(int id)
        {
            return await _context.Hoteles.FindAsync(id);
        }

        public async Task UpdateAsync(Hotel hotel)
        {
            _context.Entry(hotel).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hotel = await _context.Hoteles.FindAsync(id);
            if (hotel != null)
            {
                _context.Hoteles.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }
    }

}
