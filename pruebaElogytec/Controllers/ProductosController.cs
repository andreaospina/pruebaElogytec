using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modelosElogytec.Models;

namespace pruebaElogytec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : ControllerBase
    {

        private readonly AplicationDbContext _context;

        public ProductosController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.productos.Include(x => x.Marcas).ToListAsync());
            
        }

        [HttpGet("GetByID")]
        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                var producto = await ObtenerProductoPorId(id);

                if (producto != null)
                {
                    return Ok(producto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create( Productos producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                        _context.Add(producto);
                        await _context.SaveChangesAsync();
                        return Ok();

                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return StatusCode(500, "Ha ocurrido un error");
            }
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Productos producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Producto = await ObtenerProductoPorId(producto.Id);

                    if (Producto != null)
                    {
                        Producto.Descripcion = producto.Descripcion;
                        Producto.Ean = producto.Ean;
                        Producto.Costo = producto.Costo;
                        Producto.Precio = producto.Precio;
                        Producto.IdMarca = producto.IdMarca;

                        _context.Update(Producto);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    else { 
                        return BadRequest(); 
                    }
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch
            {
                return StatusCode(500, "Ha ocurrido un error");
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var producto = await ObtenerProductoPorId(id);
                if (producto != null)
                {
                    _context.Remove(producto);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error");
            }
        
        }

        private async Task<Productos> ObtenerProductoPorId(int id)
        {
            var producto = await _context.productos.FirstOrDefaultAsync(p => p.Id == id);
            return producto;
        }
    }
}
