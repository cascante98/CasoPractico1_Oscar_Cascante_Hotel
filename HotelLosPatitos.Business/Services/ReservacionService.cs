using HotelLosPatitos.DataAccess.Repositories;
using HotelLosPatitos.Entities;

namespace HotelLosPatitos.Business.Services;

public class ReservacionService : IReservacionService
{
    private readonly IReservacionRepository _reservacionRepository;
    private readonly IHabitacionRepository _habitacionRepository;

    public ReservacionService(
        IReservacionRepository reservacionRepository,
        IHabitacionRepository habitacionRepository)
    {
        _reservacionRepository = reservacionRepository;
        _habitacionRepository = habitacionRepository;
    }

    public async Task<List<Habitacion>> ListarHabitacionesDisponiblesAsync(
        DateTime? inicio = null, DateTime? fin = null)
    {
        var todas = await _habitacionRepository.ObtenerTodasAsync();
        var activas = todas.Where(h => h.Estado).ToList(); 

       
        if (inicio is null || fin is null)
            return activas;

    
        var ocupadas = await _reservacionRepository
            .ObtenerIdsHabitacionesOcupadasAsync(inicio.Value, fin.Value);

        return activas.Where(h => !ocupadas.Contains(h.Id)).ToList();
    }

    public async Task<Habitacion?> ObtenerHabitacionAsync(int idHabitacion)
    {
        return await _habitacionRepository.ObtenerPorIdAsync(idHabitacion);
    }

    public async Task<Reservacion?> BuscarPorIdAsync(int id)
    {
        return await _reservacionRepository.ObtenerPorIdAsync(id);
    }

    public async Task<List<Reservacion>> ListarTodasAsync(int? idHabitacion = null)
    {
        var todas = await _reservacionRepository.ObtenerTodasAsync();
        if (idHabitacion.HasValue)
            todas = todas.Where(r => r.IdHabitacion == idHabitacion.Value).ToList();
        return todas;
    }

    public async Task RegistrarAsync(Reservacion reservacion)
    {
        var habitacion = await _habitacionRepository.ObtenerPorIdAsync(reservacion.IdHabitacion);

        if (habitacion is null)
            throw new InvalidOperationException("La habitación seleccionada no existe.");

        if (!habitacion.Estado)
            throw new InvalidOperationException("No se puede reservar una habitación inactiva.");

        ValidarDatos(reservacion);

        bool yaReservada = await _reservacionRepository.ExisteReservaEnRangoAsync(
    reservacion.IdHabitacion,
    reservacion.FechaInicioReserva,
    reservacion.FechaFinReserva);

        if (yaReservada)
            throw new InvalidOperationException(
                "La habitación ya está reservada en las fechas seleccionadas. Por favor elija otras fechas.");

        
        int cantidadDiasReserva = (reservacion.FechaFinReserva - reservacion.FechaInicioReserva).Days;

        
        reservacion.MontoTotal =
            (cantidadDiasReserva * habitacion.CostoDeReserva) + habitacion.CostoDeLimpieza;

        reservacion.FechaDeRegistro = DateTime.Now; 

        await _reservacionRepository.AgregarAsync(reservacion);
    }

    private void ValidarDatos(Reservacion reservacion)
    {
        if (string.IsNullOrWhiteSpace(reservacion.NombreDeLaPersona))
            throw new ArgumentException("El nombre de la persona es obligatorio.");

        if (string.IsNullOrWhiteSpace(reservacion.Identificacion))
            throw new ArgumentException("La identificación es obligatoria.");

        if (string.IsNullOrWhiteSpace(reservacion.Telefono))
            throw new ArgumentException("El teléfono es obligatorio.");

        if (string.IsNullOrWhiteSpace(reservacion.Correo))
            throw new ArgumentException("El correo es obligatorio.");

        if (reservacion.FechaInicioReserva >= reservacion.FechaFinReserva)
            throw new ArgumentException("La fecha de inicio debe ser anterior a la fecha de fin.");

        if (reservacion.FechaInicioReserva.Date < DateTime.Now.Date)
            throw new ArgumentException("La fecha de inicio no puede ser en el pasado.");
    }
}