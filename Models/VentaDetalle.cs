using System.Text.Json.Serialization;

namespace ApiVenta.Models
{
    public class VentaDetalle
    {
        public int Id { get; set; }
        public int VentaId { get; set; }

        [JsonIgnore]
        public Venta Venta { get; set; }
        public int ProductoId { get; set; }

        [JsonIgnore]
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
    }
}
