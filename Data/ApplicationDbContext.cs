using ApiVenta.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiVenta.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //relacionando la tabla con la entidad 
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Venta> Venta { get; set; }
        public DbSet<VentaDetalle> VentaDetalle { get; set; }
    }
}
