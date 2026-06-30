using HotelLosPatitos.Entities;

namespace HotelLosPatitos.DataAccess.Repositories;

public interface IHabitacionRepository
{
    Task<List<Habitacion>> ObtenerTodasAsync();
    Task<Habitacion?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Habitacion habitacion);
    Task ActualizarAsync(Habitacion habitacion);

    Task<Habitacion?> ObtenerUltimaAsync();

 
}