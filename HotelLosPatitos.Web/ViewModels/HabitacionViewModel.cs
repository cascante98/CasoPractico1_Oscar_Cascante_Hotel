using System.ComponentModel.DataAnnotations;

namespace HotelLosPatitos.Web.ViewModels;

public class HabitacionViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El código es obligatorio.")]
    [StringLength(7, ErrorMessage = "El código no puede superar los 7 caracteres.")]
    [Display(Name = "Código de habitación")]
    public string CodigoDeHabitacion { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(30, ErrorMessage = "El nombre no puede superar los 30 caracteres.")]
    [Display(Name = "Nombre de habitación")]
    public string NombreDeHabitacion { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Debe ser mayor a 0.")]
    [Display(Name = "Cantidad de huéspedes permitidos")]
    public int CantidadDeHuespedesPermitidos { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Debe ser mayor a 0.")]
    [Display(Name = "Cantidad de camas")]
    public int CantidadDeCamas { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Debe ser mayor a 0.")]
    [Display(Name = "Cantidad de baños")]
    public int CantidadDeBanos { get; set; }

    [Required(ErrorMessage = "La ubicación es obligatoria.")]
    [StringLength(10, ErrorMessage = "La ubicación no puede superar los 10 caracteres.")]
    public string Ubicacion { get; set; } = null!;

    [Required(ErrorMessage = "El encargado de limpieza es obligatorio.")]
    [StringLength(100)]
    [Display(Name = "Encargado de limpieza")]
    public string EncargadoDeLimpieza { get; set; } = null!;

    [Required]
    [Range(1, 3, ErrorMessage = "Seleccione un tipo válido.")]
    [Display(Name = "Tipo de habitación")]
    public int TipoDeHabitacion { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Debe ser mayor a 0.")]
    [Display(Name = "Costo de limpieza")]
    public decimal CostoDeLimpieza { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Debe ser mayor a 0.")]
    [Display(Name = "Costo de reserva")]
    public decimal CostoDeReserva { get; set; }

    public bool Estado { get; set; } = true;
}