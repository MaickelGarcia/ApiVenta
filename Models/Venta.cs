namespace ApiVenta.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public ICollection<VentaDetalle> VentaDetalles { get; set; }
    }
}
