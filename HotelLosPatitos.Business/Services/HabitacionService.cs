using HotelLosPatitos.DataAccess.Repositories;
using HotelLosPatitos.Entities;

namespace HotelLosPatitos.Business.Services;

public class HabitacionService : IHabitacionService
{
    private readonly IHabitacionRepository _habitacionRepository;

    public HabitacionService(IHabitacionRepository habitacionRepository)
    {
        _habitacionRepository = habitacionRepository;
    }

    public async Task<List<Habitacion>> ListarAsync()
    {
        return await _habitacionRepository.ObtenerTodasAsync();
    }

    public async Task<Habitacion?> ObtenerPorIdAsync(int id)
    {
        return await _habitacionRepository.ObtenerPorIdAsync(id);
    }

    public async Task RegistrarAsync(Habitacion habitacion)
    {
        habitacion.CodigoDeHabitacion = await ObtenerProximoCodigoAsync();

        ValidarDatos(habitacion);


        
        habitacion.FechaDeRegistro = DateTime.Now;
        habitacion.FechaDeModificacion = null;
        habitacion.Estado = true; 

        await _habitacionRepository.AgregarAsync(habitacion);
    }

    public async Task EditarAsync(Habitacion habitacion)
    {
        var existente = await _habitacionRepository.ObtenerPorIdAsync(habitacion.Id);
        if (existente is null)
            throw new InvalidOperationException("La habitación que intenta editar no existe.");

       
        existente.CantidadDeHuespedesPermitidos = habitacion.CantidadDeHuespedesPermitidos;
        existente.CantidadDeCamas = habitacion.CantidadDeCamas;
        existente.EncargadoDeLimpieza = habitacion.EncargadoDeLimpieza;
        existente.TipoDeHabitacion = habitacion.TipoDeHabitacion;
        existente.CostoDeLimpieza = habitacion.CostoDeLimpieza;
        existente.CostoDeReserva = habitacion.CostoDeReserva;
        existente.Estado = habitacion.Estado;
        existente.FechaDeModificacion = DateTime.Now; 

        ValidarDatos(existente);

        await _habitacionRepository.ActualizarAsync(existente);
    }

    private void ValidarDatos(Habitacion habitacion)
    {
        if (habitacion.CantidadDeHuespedesPermitidos <= 0)
            throw new ArgumentException("La cantidad de huéspedes permitidos debe ser mayor a 0.");

        if (habitacion.TipoDeHabitacion is < 1 or > 3)
            throw new ArgumentException("El tipo de habitación debe ser 1 (Junior), 2 (Superior) o 3 (Suite).");

        if (habitacion.CostoDeLimpieza <= 0)
            throw new ArgumentException("El costo de limpieza debe ser mayor a 0.");

        if (habitacion.CostoDeReserva <= 0)
            throw new ArgumentException("El costo de reserva debe ser mayor a 0.");

        if (string.IsNullOrWhiteSpace(habitacion.EncargadoDeLimpieza))
            throw new ArgumentException("El encargado de limpieza es obligatorio.");
    }

    public async Task<string> ObtenerProximoCodigoAsync()
    {
        var ultima = await _habitacionRepository.ObtenerUltimaAsync();

        int siguienteNumero = 1;
        if (ultima is not null && ultima.CodigoDeHabitacion.StartsWith("HAB-"))
        {
           
            var parteNumerica = ultima.CodigoDeHabitacion.Substring(4);
            if (int.TryParse(parteNumerica, out int numeroActual))
                siguienteNumero = numeroActual + 1;
        }

        return $"HAB-{siguienteNumero:D3}"; 
    }
}