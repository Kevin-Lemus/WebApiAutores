using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libro")]
    public class LibroController : Controller
    {
        private readonly AplicationContext _context;

        public LibroController(AplicationContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")] // Ruta: api/libro/id
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var exist = await _context.Libros.AnyAsync(x => x.Id == id);

            if (!exist) return NotFound();

            return await _context.Libros.Include(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await _context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existeAutor) return BadRequest("Autor no encontrado");

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
