using HotelLosPatitos.Entities;

namespace HotelLosPatitos.Business.Services;

public interface IHabitacionService
{
    Task<List<Habitacion>> ListarAsync();
    Task<Habitacion?> ObtenerPorIdAsync(int id);
    Task RegistrarAsync(Habitacion habitacion);
    Task EditarAsync(Habitacion habitacion);

    Task<string> ObtenerProximoCodigoAsync();
}