using HotelLosPatitos.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelLosPatitos.DataAccess.Repositories;

public class ReservacionRepository : IReservacionRepository
{
    private readonly ReservacionesDbContext _context;

    public ReservacionRepository(ReservacionesDbContext context)
    {
        _context = context;
    }

    public async Task<Reservacion?> ObtenerPorIdAsync(int id)
    {
        return await _context.Reservaciones
            .Include(r => r.Habitacion)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Reservacion>> ObtenerTodasAsync()
    {
        return await _context.Reservaciones
            .Include(r => r.Habitacion)
            .ToListAsync();
    }

    public async Task AgregarAsync(Reservacion reservacion)
    {
        _context.Reservaciones.Add(reservacion);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteReservaEnRangoAsync(int idHabitacion, DateTime inicio, DateTime fin)
    {
        return await _context.Reservaciones.AnyAsync(r =>
            r.IdHabitacion == idHabitacion &&
            r.FechaInicioReserva < fin &&
            inicio < r.FechaFinReserva);
    }

    public async Task<List<int>> ObtenerIdsHabitacionesOcupadasAsync(DateTime inicio, DateTime fin)
    {
        return await _context.Reservaciones
            .Where(r => r.FechaInicioReserva < fin && inicio < r.FechaFinReserva)
            .Select(r => r.IdHabitacion)
            .Distinct()
            .ToListAsync();
    }
}