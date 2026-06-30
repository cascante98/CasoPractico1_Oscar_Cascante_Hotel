using HotelLosPatitos.Business.Services;
using HotelLosPatitos.Entities;
using HotelLosPatitos.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelLosPatitos.Web.Controllers;

public class HabitacionesController : Controller
{
    private readonly IHabitacionService _habitacionService;

    public HabitacionesController(IHabitacionService habitacionService)
    {
        _habitacionService = habitacionService;
    }

    // GET: /Habitaciones
    public async Task<IActionResult> Index()
    {
        var habitaciones = await _habitacionService.ListarAsync();
        return View(habitaciones);
    }

    // GET: /Habitaciones/Registrar
    public async Task<IActionResult> Registrar()
    {
        var modelo = new HabitacionViewModel
        {
            CodigoDeHabitacion = await _habitacionService.ObtenerProximoCodigoAsync()
        };
        return View(modelo);
    }

    // POST: /Habitaciones/Registrar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registrar(HabitacionViewModel modelo)
    {
        if (!ModelState.IsValid)
            return View(modelo);

        try
        {
            var habitacion = new Habitacion
            {
                CodigoDeHabitacion = modelo.CodigoDeHabitacion,
                NombreDeHabitacion = modelo.NombreDeHabitacion,
                CantidadDeHuespedesPermitidos = modelo.CantidadDeHuespedesPermitidos,
                CantidadDeCamas = modelo.CantidadDeCamas,
                CantidadDeBanos = modelo.CantidadDeBanos,
                Ubicacion = modelo.Ubicacion,
                EncargadoDeLimpieza = modelo.EncargadoDeLimpieza,
                TipoDeHabitacion = modelo.TipoDeHabitacion,
                CostoDeLimpieza = modelo.CostoDeLimpieza,
                CostoDeReserva = modelo.CostoDeReserva
            };

            await _habitacionService.RegistrarAsync(habitacion);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(modelo);
        }
    }

    // GET: /Habitaciones/Editar/5
    public async Task<IActionResult> Editar(int id)
    {
        var habitacion = await _habitacionService.ObtenerPorIdAsync(id);
        if (habitacion is null)
            return RedirectToAction(nameof(Index));

        var modelo = new HabitacionViewModel
        {
            Id = habitacion.Id,
            CodigoDeHabitacion = habitacion.CodigoDeHabitacion,
            NombreDeHabitacion = habitacion.NombreDeHabitacion,
            CantidadDeHuespedesPermitidos = habitacion.CantidadDeHuespedesPermitidos,
            CantidadDeCamas = habitacion.CantidadDeCamas,
            CantidadDeBanos = habitacion.CantidadDeBanos,
            Ubicacion = habitacion.Ubicacion,
            EncargadoDeLimpieza = habitacion.EncargadoDeLimpieza,
            TipoDeHabitacion = habitacion.TipoDeHabitacion,
            CostoDeLimpieza = habitacion.CostoDeLimpieza,
            CostoDeReserva = habitacion.CostoDeReserva,
            Estado = habitacion.Estado
        };
        return View(modelo);
    }

    // POST: /Habitaciones/Editar/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(HabitacionViewModel modelo)
    {
        if (!ModelState.IsValid)
            return View(modelo);

        try
        {
            var habitacion = new Habitacion
            {
                Id = modelo.Id,
                CantidadDeHuespedesPermitidos = modelo.CantidadDeHuespedesPermitidos,
                CantidadDeCamas = modelo.CantidadDeCamas,
                EncargadoDeLimpieza = modelo.EncargadoDeLimpieza,
                TipoDeHabitacion = modelo.TipoDeHabitacion,
                CostoDeLimpieza = modelo.CostoDeLimpieza,
                CostoDeReserva = modelo.CostoDeReserva,
                Estado = modelo.Estado
            };

            await _habitacionService.EditarAsync(habitacion);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(modelo);
        }
    }
}