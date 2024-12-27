using ApiVenta.Data;
using ApiVenta.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiVenta.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        //constructor
        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/products
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Producto);
        }

        // GET: api/v1/products/5
        [HttpGet("{id}", Name = "GetProducto")]
        public IActionResult Get(int id)
        {
            var producto = _context.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }


        // POST: api/v1/products
        [HttpPost]
        public IActionResult Post([FromBody] Producto producto)
        {
            _context.Producto.Add(producto);
            _context.SaveChanges();
            return CreatedAtRoute("GetProducto", new { id = producto.Id }, producto);
        }

    }
}
