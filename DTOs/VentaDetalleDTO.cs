using System.ComponentModel.DataAnnotations;

namespace ApiVenta.DTOs
{
    public class VentaDetalleDTO
    {
        [Required]
        public int ProductoId { get; set; }
        [Required]
        [Range( 1, int.MaxValue, ErrorMessage ="La cantidad debe ser mayor que 0")]
        public int Cantidad { get; set; }
    }
}
