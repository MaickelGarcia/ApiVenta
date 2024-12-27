namespace ApiVenta.DTOs
{
    public class VentaDTO
    {
        public DateTime Fecha { get; set; }
        public List<VentaDetalleDTO> Detalles { get; set; }
    }
}
