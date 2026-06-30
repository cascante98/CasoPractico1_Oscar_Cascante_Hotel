using HotelLosPatitos.Business.Services;
using HotelLosPatitos.Entities;
using HotelLosPatitos.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelLosPatitos.Web.Controllers;

public class ReservaController : Controller
{
    private readonly IReservacionService _reservacionService;

    public ReservaController(IReservacionService reservacionService)
    {
        _reservacionService = reservacionService;
    }

    // GET: /Reserva  -> lista de habitaciones disponibles
    public async Task<IActionResult> Index(DateTime? inicio, DateTime? fin)
    {
        var habitaciones = await _reservacionService
            .ListarHabitacionesDisponiblesAsync(inicio, fin);

        
        ViewBag.Inicio = inicio?.ToString("yyyy-MM-dd");
        ViewBag.Fin = fin?.ToString("yyyy-MM-dd");

        return View(habitaciones);
    }

    // POST: /Reserva/Buscar -> busca una reserva por su número
    [HttpPost]
    public async Task<IActionResult> Buscar(int idReservacion)
    {
        var reserva = await _reservacionService.BuscarPorIdAsync(idReservacion);
        if (reserva is null)
        {
            TempData["Mensaje"] = "Estimado usuario, no se ha encontrado la reservación, favor realice una";
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Detalles), new { id = reserva.Id });
    }

    // GET: /Reserva/Detalles/5
    public async Task<IActionResult> Detalles(int id)
    {
        var reserva = await _reservacionService.BuscarPorIdAsync(id);
        if (reserva is null)
            return RedirectToAction(nameof(Index));
        return View(reserva);
    }

    // GET: /Reserva/Reservar/5  (5 = IdHabitacion)
    public async Task<IActionResult> Reservar(int id)
    {
        var habitacion = await _reservacionService.ObtenerHabitacionAsync(id);
        if (habitacion is null || !habitacion.Estado)
            return RedirectToAction(nameof(Index));

        var modelo = new ReservacionViewModel
        {
            IdHabitacion = habitacion.Id,
            CodigoHabitacion = habitacion.CodigoDeHabitacion,
            FechaNacimiento = new DateTime(2000, 1, 1),
            NombreHabitacion = habitacion.NombreDeHabitacion,
            TipoHabitacionTexto = ObtenerTipoTexto(habitacion.TipoDeHabitacion),
            CapacidadHabitacion = habitacion.CantidadDeHuespedesPermitidos,
            CostoPorNoche = habitacion.CostoDeReserva,
            CostoLimpieza = habitacion.CostoDeLimpieza,
            FechaInicioReserva = DateTime.Today,
            FechaFinReserva = DateTime.Today.AddDays(1)
        };
        return View(modelo);
    }

    // POST: /Reserva/Reservar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reservar(ReservacionViewModel modelo)
    {
        if (!ModelState.IsValid)
        {
            await RecargarDatosHabitacion(modelo);
            return View(modelo);
        }

        try
        {
            var reservacion = new Reservacion
            {
                NombreDeLaPersona = modelo.NombreDeLaPersona,
                Identificacion = modelo.Identificacion,
                Telefono = modelo.Telefono,
                Correo = modelo.Correo,
                FechaNacimiento = modelo.FechaNacimiento,
                Direccion = modelo.Direccion,
                FechaInicioReserva = modelo.FechaInicioReserva,
                FechaFinReserva = modelo.FechaFinReserva,
                IdHabitacion = modelo.IdHabitacion
            };

            await _reservacionService.RegistrarAsync(reservacion);
            return RedirectToAction(nameof(Detalles), new { id = reservacion.Id });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await RecargarDatosHabitacion(modelo);
            return View(modelo);
        }
    }

    private async Task RecargarDatosHabitacion(ReservacionViewModel modelo)
    {
        var habitacion = await _reservacionService.ObtenerHabitacionAsync(modelo.IdHabitacion);
        if (habitacion is not null)
        {
            modelo.NombreHabitacion = habitacion.NombreDeHabitacion;
            modelo.TipoHabitacionTexto = ObtenerTipoTexto(habitacion.TipoDeHabitacion);
            modelo.CapacidadHabitacion = habitacion.CantidadDeHuespedesPermitidos;
            modelo.CostoPorNoche = habitacion.CostoDeReserva;
            modelo.CostoLimpieza = habitacion.CostoDeLimpieza;
            modelo.CodigoHabitacion = habitacion.CodigoDeHabitacion;
        }
    }

    private static string ObtenerTipoTexto(int tipo) => tipo switch
    {
        1 => "Junior",
        2 => "Superior",
        3 => "Suite",
        _ => "Desconocido"
    };
}