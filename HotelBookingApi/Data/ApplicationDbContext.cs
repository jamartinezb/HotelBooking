using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hoteles { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Reservacion> Reservaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<Hotel>()
                .Property(h => h.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Habitacion>()
                .HasMany(h => h.Reservaciones)
                .WithOne(r => r.Habitacion)
                .HasForeignKey(r => r.HabitacionId);

            modelBuilder.Entity<Reservacion>()
                .Property(h => h.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
