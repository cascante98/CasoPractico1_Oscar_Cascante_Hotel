using HotelLosPatitos.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelLosPatitos.DataAccess.Repositories;

public class HabitacionRepository : IHabitacionRepository
{
    private readonly ReservacionesDbContext _context;

    public HabitacionRepository(ReservacionesDbContext context)
    {
        _context = context;
    }

    public async Task<List<Habitacion>> ObtenerTodasAsync()
    {
        return await _context.Habitaciones.ToListAsync();
    }

    public async Task<Habitacion?> ObtenerPorIdAsync(int id)
    {
        return await _context.Habitaciones.FindAsync(id);
    }

    public async Task AgregarAsync(Habitacion habitacion)
    {
        _context.Habitaciones.Add(habitacion);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Habitacion habitacion)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Habitacion?> ObtenerUltimaAsync()
    {
        return await _context.Habitaciones
            .OrderByDescending(h => h.Id)
            .FirstOrDefaultAsync();
    }
}