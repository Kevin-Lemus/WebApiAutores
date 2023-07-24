using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libro")]
    public class LibroController : Controller
    {
        private readonly AplicationContext _context;
        private readonly IMapper mapper;

        public LibroController(AplicationContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<LibroDTO>> Get()
        {
            var libros = await _context.Libros.ToListAsync();
            return mapper.Map<List<LibroDTO>>(libros);
        }

        [HttpGet("{id:int}")] // Ruta: api/libro/id
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var exist = await _context.Libros.AnyAsync(x => x.Id == id);
            if (!exist) return NotFound();
            var libro = await _context.Libros.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<LibroDTO>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroDTO)
        {
            var existeAutor = await _context.Autores.AnyAsync(x => x.Id == libroDTO.AutorId);
            if (!existeAutor) return BadRequest("Autor no encontrado");
            var libro = mapper.Map<Libro>(libroDTO);
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
