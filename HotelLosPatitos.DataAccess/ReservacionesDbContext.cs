using HotelLosPatitos.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelLosPatitos.DataAccess;

public class ReservacionesDbContext : DbContext
{
    public ReservacionesDbContext(DbContextOptions<ReservacionesDbContext> options)
        : base(options) { }

    public DbSet<Habitacion> Habitaciones => Set<Habitacion>();
    public DbSet<Reservacion> Reservaciones => Set<Reservacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.ToTable("HABITACIONES");          
            entity.HasKey(h => h.Id);
            entity.Property(h => h.CostoDeLimpieza).HasColumnType("decimal(18,2)");
            entity.Property(h => h.CostoDeReserva).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Reservacion>(entity =>
        {
            entity.ToTable("RESERVACIONES");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.MontoTotal).HasColumnType("decimal(18,2)");
            entity.HasOne(r => r.Habitacion)
                  .WithMany()
                  .HasForeignKey(r => r.IdHabitacion);
        });

        base.OnModelCreating(modelBuilder);
    }
}