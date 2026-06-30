using System.ComponentModel.DataAnnotations;

namespace HotelLosPatitos.Web.ViewModels;

public class ReservacionViewModel
{
    public int IdHabitacion { get; set; } 

    
    public string? NombreHabitacion { get; set; }
    public string? TipoHabitacionTexto { get; set; }
    public int CapacidadHabitacion { get; set; }
    public decimal CostoPorNoche { get; set; }
    public decimal CostoLimpieza { get; set; }

    public string? CodigoHabitacion { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(150)]
    [Display(Name = "Nombre de la persona")]
    public string NombreDeLaPersona { get; set; } = null!;

    [Required(ErrorMessage = "La identificación es obligatoria.")]
    [StringLength(30)]
    [Display(Name = "Identificación")]
    public string Identificacion { get; set; } = null!;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [StringLength(10)]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = null!;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [StringLength(50)]
    [EmailAddress(ErrorMessage = "El correo no es válido.")]
    [Display(Name = "Correo")]
    public string Correo { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de nacimiento")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(200)]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de inicio de reserva")]
    public DateTime FechaInicioReserva { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de fin de reserva")]
    public DateTime FechaFinReserva { get; set; }
}