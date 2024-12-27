using ApiVenta.Data;
using ApiVenta.DTOs;
using ApiVenta.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVenta.Controllers
{
    [Route("api/v1/ventas")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/ventas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ventas = await _context.Venta
                                .Include(v => v.VentaDetalles)
                                .ThenInclude(d => d.Producto)
                                .ToListAsync();
        
            return Ok(ventas);
        }

        // GET: api/v1/ventas/5
        [HttpGet("{id}", Name = "GetVenta")]
        public async Task<IActionResult> Get(int id)
        {
            var venta = await _context.Venta
                                .Include(v => v.VentaDetalles)
                                .ThenInclude(d => d.Producto)
                                .FirstOrDefaultAsync(v => v.Id == id);
            if (venta == null)
            {
                return NotFound();
            }
            return Ok(venta);
        }

        // POST: api/v1/ventas
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VentaDTO venta)
        { 
            if (venta.Detalles == null || !venta.Detalles.Any())
               return BadRequest("No hay productos en la venta");

            var ventaNueva = new Venta
            {
                Fecha = DateTime.Now,
                VentaDetalles = new List<VentaDetalle>()
            };

            decimal total = 0;

            foreach(var detalle in venta.Detalles)
            {
                //if(detalle.Cantidad <= 0)
                //{
                //    return BadRequest("La cantidad de productos debe ser mayor a 0");
                //}

                var producto = await _context.Producto.FirstOrDefaultAsync(p => p.Id == detalle.ProductoId);
                if (producto == null)
                {
                    return BadRequest();
                }

                if(producto.Cantidad < detalle.Cantidad) 
                {
                    return BadRequest("No hay suficiente stock para el producto " + producto.Nombre);
                }

                producto.Cantidad -= detalle.Cantidad;

                var ventaDetalle = new VentaDetalle
                {
                    ProductoId = producto.Id,
                    Producto = producto,
                    Cantidad = detalle.Cantidad,
                    SubTotal = producto.Precio * detalle.Cantidad
                };
                total += ventaDetalle.SubTotal;
                ventaNueva.VentaDetalles.Add(ventaDetalle);

            }

            ventaNueva.Total = total;

            await _context.Venta.AddAsync(ventaNueva);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("GetVenta", new { id = ventaNueva.Id }, ventaNueva);
        }

    }
}
