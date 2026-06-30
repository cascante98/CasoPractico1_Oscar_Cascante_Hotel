using HotelLosPatitos.Entities;

namespace HotelLosPatitos.Business.Services;

public interface IReservacionService
{
    Task<List<Habitacion>> ListarHabitacionesDisponiblesAsync(DateTime? inicio = null, DateTime? fin = null);
    Task<Habitacion?> ObtenerHabitacionAsync(int idHabitacion);
    Task<Reservacion?> BuscarPorIdAsync(int id);
    Task<List<Reservacion>> ListarTodasAsync(int? idHabitacion = null);
    Task RegistrarAsync(Reservacion reservacion);
}