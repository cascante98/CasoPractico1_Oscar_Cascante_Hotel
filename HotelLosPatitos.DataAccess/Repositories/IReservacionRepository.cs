using HotelLosPatitos.Entities;

namespace HotelLosPatitos.DataAccess.Repositories;

public interface IReservacionRepository
{
    Task<Reservacion?> ObtenerPorIdAsync(int id);
    Task<List<Reservacion>> ObtenerTodasAsync();
    Task AgregarAsync(Reservacion reservacion);

    Task<bool> ExisteReservaEnRangoAsync(int idHabitacion, DateTime inicio, DateTime fin);

    Task<List<int>> ObtenerIdsHabitacionesOcupadasAsync(DateTime inicio, DateTime fin);
}