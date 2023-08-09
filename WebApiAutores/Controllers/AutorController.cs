using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;
using WebApiAutores.Utilidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autor")]
    public class AutorController : Controller
    {
        private readonly AplicationContext _context;
        private readonly IMapper _mapper;

        public AutorController(AplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> Get()
        {
            var autores = await _context.Autores.ToListAsync();
            return _mapper.Map<List<AutorDTO>>(autores);
        }

        [HttpGet("{id:int}", Name = "ObtenerAutor")]
        public async Task<ActionResult<FullAutorDTO>> Get([FromRoute] int id)
        {
            var autor = await _context.Autores
                                        .Include(autorDb => autorDb.LibrosAutores)
                                        .ThenInclude(libroAutorDb => libroAutorDb.Libro)
                                        .FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null) return NotFound();

            return _mapper.Map<FullAutorDTO>(autor);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<FullAutorDTO>>> Get([FromRoute] string nombre)
        {
            var autores = await _context.Autores
                          .Where(autor => autor.Nombre.Contains(nombre)).ToListAsync();
            if (!autores.Any()) return NotFound();
            return _mapper.Map<List<FullAutorDTO>>(autores);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AutorCreacionDTO autorDTO)
        {
            var autor = _mapper.Map<Autor>(autorDTO);
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            var autorRespuesta = _mapper.Map<AutorDTO>(autor);

            return CreatedAtRoute("ObtenerAutor", new {id = autor.Id}, autorRespuesta);
        }

        [HttpPut("{id:int}")] // api/autor/id
        public async Task<IActionResult> Put(AutorCreacionDTO autorDTO, int id)
        {
            var exist = await _context.Autores.AnyAsync(x => x.Id == id);

            if(!exist) return NotFound();

            var autor = _mapper.Map<Autor>(autorDTO);
            autor.Id = id;

            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")] // api/autor/id
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _context.Autores.AnyAsync(x => x.Id == id);

            if (!exist) return NotFound();

            var autorEliminado = new Autor(){ Id = id };

            _context.Autores.Remove(autorEliminado);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
