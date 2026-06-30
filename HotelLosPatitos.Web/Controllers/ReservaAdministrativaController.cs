using HotelLosPatitos.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelLosPatitos.Web.Controllers;

public class ReservaAdministrativaController : Controller
{
    private readonly IReservacionService _reservacionService;
    private readonly IHabitacionService _habitacionService;

    public ReservaAdministrativaController(
        IReservacionService reservacionService,
        IHabitacionService habitacionService)
    {
        _reservacionService = reservacionService;
        _habitacionService = habitacionService;
    }

    public async Task<IActionResult> Index(int? id)
    {
      
        var habitaciones = await _habitacionService.ListarAsync();
        ViewBag.Habitaciones = new SelectList(habitaciones, "Id", "NombreDeHabitacion", id);
        ViewBag.IdSeleccionado = id;

        
        var reservas = await _reservacionService.ListarTodasAsync(id);
        return View(reservas);
    }
}