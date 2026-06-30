namespace HotelLosPatitos.Entities;

public class Habitacion
{
    public int Id { get; set; }
    public string CodigoDeHabitacion { get; set; } = null!;
    public string NombreDeHabitacion { get; set; } = null!;
    public int CantidadDeHuespedesPermitidos { get; set; }
    public int CantidadDeCamas { get; set; }
    public int CantidadDeBanos { get; set; }
    public string Ubicacion { get; set; } = null!;
    public string EncargadoDeLimpieza { get; set; } = null!;
    public int TipoDeHabitacion { get; set; }       
    public decimal CostoDeLimpieza { get; set; }
    public decimal CostoDeReserva { get; set; }
    public DateTime FechaDeRegistro { get; set; }
    public DateTime? FechaDeModificacion { get; set; }  
    public bool Estado { get; set; }                 
}